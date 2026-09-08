using Application.Features.Orders.DTOs;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Features.Orders.Service
{
    public class OrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderService> _logger;

        public OrderService(ApplicationDbContext context, ILogger<OrderService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<OrderDto> CreateOrderAsync(Guid userId, CreateOrderDto dto)
        {
            var idempotencyKey = string.IsNullOrWhiteSpace(dto.IdempotencyKey)
                ? Guid.NewGuid().ToString()
                : dto.IdempotencyKey.Trim();

            // Check if an order with this idempotency key already exists for user
            var existingOrder = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.UserId == userId && o.IdempotencyKey == idempotencyKey);

            if (existingOrder != null)
            {
                _logger.LogInformation(
                    "Idempotent order create: returning existing order {OrderId} for user {UserId} (key {IdempotencyKey}).",
                    existingOrder.Id, userId, idempotencyKey);
                return MapToDto(existingOrder);
            }

            var orderItems = new List<OrderItem>();
            decimal totalAmount = 0m;

            if (dto.Items != null && dto.Items.Count > 0)
            {
                foreach (var itemDto in dto.Items)
                {
                    var product = await _context.Products
                        .Include(p => p.Variants)
                        .FirstOrDefaultAsync(p => p.Id == itemDto.ProductId);

                    if (product == null)
                    {
                        throw new KeyNotFoundException($"Product with ID {itemDto.ProductId} was not found.");
                    }

                    var variant = product.Variants.FirstOrDefault();
                    if (variant != null && variant.StockQuantity < itemDto.Quantity)
                    {
                        _logger.LogWarning(
                            "Order create failed for user {UserId}: insufficient stock for variant {VariantId} ('{ProductName}'). Requested {Requested}, available {Available}.",
                            userId, variant.Id, product.Name, itemDto.Quantity, variant.StockQuantity);
                        throw new InvalidOperationException($"Insufficient stock for product '{product.Name}'. Available: {variant.StockQuantity}");
                    }

                    if (variant != null)
                    {
                        variant.StockQuantity -= itemDto.Quantity;
                        _context.ProductVariants.Update(variant);
                    }

                    var itemTotal = product.Price * itemDto.Quantity;
                    totalAmount += itemTotal;

                    orderItems.Add(new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Color = variant?.Color,
                        UnitPrice = product.Price,
                        Quantity = itemDto.Quantity
                    });
                }
            }
            else
            {
                // Checkout from user's Cart
                var cart = await _context.Carts
                    .Include(c => c.Items)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (cart == null || cart.Items.Count == 0)
                {
                    throw new InvalidOperationException("Cannot create an order with an empty cart.");
                }

                foreach (var cartItem in cart.Items)
                {
                    var variant = await _context.ProductVariants.FindAsync(cartItem.ProductVariantId);
                    if (variant != null && variant.StockQuantity < cartItem.Quantity)
                    {
                        _logger.LogWarning(
                            "Checkout failed for user {UserId}: insufficient stock for variant {VariantId} ('{ProductName}'). Requested {Requested}, available {Available}.",
                            userId, variant.Id, cartItem.ProductName, cartItem.Quantity, variant.StockQuantity);
                        throw new InvalidOperationException($"Insufficient stock for product '{cartItem.ProductName}' ({cartItem.Color}). Available: {variant.StockQuantity}");
                    }

                    if (variant != null)
                    {
                        variant.StockQuantity -= cartItem.Quantity;
                        _context.ProductVariants.Update(variant);
                    }

                    var itemTotal = cartItem.UnitPrice * cartItem.Quantity;
                    totalAmount += itemTotal;

                    orderItems.Add(new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = cartItem.ProductId,
                        ProductName = cartItem.ProductName,
                        Color = cartItem.Color,
                        UnitPrice = cartItem.UnitPrice,
                        Quantity = cartItem.Quantity
                    });
                }

                // Clear cart items after checkout
                _context.CartItems.RemoveRange(cart.Items);
                cart.UpdatedAt = DateTime.UtcNow;
                _context.Carts.Update(cart);
            }

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                IdempotencyKey = idempotencyKey,
                // Must match CK_Orders_Status ('Pending','Paid','Failed','Cancelled').
                Status = "Pending",
                TotalAmount = totalAmount,
                ShippingAddress = dto.ShippingAddress,
                CreatedAt = DateTime.UtcNow,
                Items = orderItems
            };

            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // Two requests with the same IdempotencyKey raced past the existence
                // check above; the unique index on Orders.IdempotencyKey rejected this
                // one. The other request won - return its order instead of failing.
                _context.ChangeTracker.Clear();

                Order? winner = null;
                for (var attempt = 0; attempt < 3 && winner == null; attempt++)
                {
                    winner = await _context.Orders
                        .Include(o => o.Items)
                        .FirstOrDefaultAsync(o => o.UserId == userId && o.IdempotencyKey == idempotencyKey);
                    if (winner == null) await Task.Delay(50);
                }

                if (winner == null) throw;

                _logger.LogInformation(
                    "Concurrent idempotent order create for user {UserId} (key {IdempotencyKey}); returning winning order {OrderId}.",
                    userId, idempotencyKey, winner.Id);
                return MapToDto(winner);
            }

            _logger.LogInformation(
                "Order {OrderId} created for user {UserId}: {ItemCount} item(s), total {TotalAmount}.",
                order.Id, userId, order.Items.Count, order.TotalAmount);

            return MapToDto(order);
        }

        public async Task<OrderDto?> GetOrderAsync(Guid userId, Guid orderId)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null || order.UserId != userId)
            {
                // Ownership check: return null if order doesn't exist or belongs to another user
                return null;
            }

            return MapToDto(order);
        }

        public async Task<List<OrderDto>> GetUserOrdersAsync(Guid userId)
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            return orders.Select(MapToDto).ToList();
        }

        private static OrderDto MapToDto(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                IdempotencyKey = order.IdempotencyKey,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                ShippingAddress = order.ShippingAddress,
                CreatedAt = order.CreatedAt,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    Id = i.Id,
                    OrderId = i.OrderId,
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Color = i.Color,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                }).ToList()
            };
        }
    }
}
