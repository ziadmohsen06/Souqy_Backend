using Application.Features.Orders.DTOs;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Orders.Service
{
    public class OrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
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
                return MapToDto(existingOrder);
            }

            var orderItems = new List<OrderItem>();
            decimal totalAmount = 0m;

            if (dto.Items != null && dto.Items.Count > 0)
            {
                foreach (var itemDto in dto.Items)
                {
                    var product = await _context.Products.FindAsync(itemDto.ProductId);
                    if (product == null)
                    {
                        throw new KeyNotFoundException($"Product with ID {itemDto.ProductId} was not found.");
                    }

                    if (product.StockQuantity < itemDto.Quantity)
                    {
                        throw new InvalidOperationException($"Insufficient stock for product '{product.Name}'. Available: {product.StockQuantity}");
                    }

                    // Reserve stock
                    product.StockQuantity -= itemDto.Quantity;

                    var itemTotal = product.Price * itemDto.Quantity;
                    totalAmount += itemTotal;

                    orderItems.Add(new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = product.Id,
                        ProductName = product.Name,
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
                    .ThenInclude(i => i.Product)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (cart == null || cart.Items.Count == 0)
                {
                    throw new InvalidOperationException("Cannot create an order with an empty cart.");
                }

                foreach (var cartItem in cart.Items)
                {
                    if (cartItem.Product == null) continue;

                    if (cartItem.Product.StockQuantity < cartItem.Quantity)
                    {
                        throw new InvalidOperationException($"Insufficient stock for product '{cartItem.Product.Name}'. Available: {cartItem.Product.StockQuantity}");
                    }

                    cartItem.Product.StockQuantity -= cartItem.Quantity;

                    var itemTotal = cartItem.Product.Price * cartItem.Quantity;
                    totalAmount += itemTotal;

                    orderItems.Add(new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = cartItem.Product.Id,
                        ProductName = cartItem.Product.Name,
                        UnitPrice = cartItem.Product.Price,
                        Quantity = cartItem.Quantity
                    });
                }

                // Clear cart items after checkout
                _context.CartItems.RemoveRange(cart.Items);
            }

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                IdempotencyKey = idempotencyKey,
                Status = OrderStatus.PendingPayment.ToString(),
                TotalAmount = totalAmount,
                ShippingAddress = dto.ShippingAddress,
                CreatedAt = DateTime.UtcNow,
                Items = orderItems
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

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
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                }).ToList()
            };
        }
    }
}
