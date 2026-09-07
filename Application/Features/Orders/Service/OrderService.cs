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
            // Get user's cart with items
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.Items.Any())
            {
                throw new InvalidOperationException("Cart is empty. Cannot create order.");
            }

            // Validate stock availability for all items
            foreach (var item in cart.Items)
            {
                var variant = await _context.ProductVariants.FindAsync(item.ProductVariantId);
                if (variant == null || variant.StockQuantity < item.Quantity)
                {
                    throw new InvalidOperationException(
                        $"Insufficient stock available for order. Item: {item.ProductName} ({item.Color})");
                }
            }

            // Generate idempotency key
            var idempotencyKey = Guid.NewGuid().ToString();

            // Create order
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                IdempotencyKey = idempotencyKey,
                Status = "Pending",
                TotalAmount = 0,
                ShippingAddress = dto.ShippingAddress,
                CreatedAt = DateTime.UtcNow,
                Items = new List<OrderItem>()
            };

            // Convert cart items to order items and deduct stock
            decimal totalAmount = 0;
            foreach (var cartItem in cart.Items)
            {
                var orderItem = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductId = cartItem.ProductId,
                    ProductName = cartItem.ProductName,
                    Color = cartItem.Color,
                    UnitPrice = cartItem.UnitPrice,
                    Quantity = cartItem.Quantity
                };
                order.Items.Add(orderItem);
                totalAmount += cartItem.UnitPrice * cartItem.Quantity;

                // Deduct stock from product variant
                var variant = await _context.ProductVariants.FindAsync(cartItem.ProductVariantId);
                if (variant != null)
                {
                    variant.StockQuantity -= cartItem.Quantity;
                    _context.ProductVariants.Update(variant);
                }
            }

            order.TotalAmount = totalAmount;

            // Save order
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Clear cart
            _context.CartItems.RemoveRange(cart.Items);
            cart.UpdatedAt = DateTime.UtcNow;
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();

            // Return order DTO
            return await GetOrderAsync(userId, order.Id);
        }

        public async Task<OrderDto?> GetOrderAsync(Guid userId, Guid orderId)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

            if (order == null)
            {
                return null;
            }

            return new OrderDto
            {
                Id = order.Id,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                ShippingAddress = order.ShippingAddress,
                CreatedAt = order.CreatedAt,
                Items = order.Items.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    OrderId = oi.OrderId,
                    ProductId = oi.ProductId,
                    ProductName = oi.ProductName,
                    Color = oi.Color,
                    UnitPrice = oi.UnitPrice,
                    Quantity = oi.Quantity
                }).ToList()
            };
        }

        public async Task<List<OrderDto>> GetUserOrdersAsync(Guid userId)
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            return orders.Select(order => new OrderDto
            {
                Id = order.Id,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                ShippingAddress = order.ShippingAddress,
                CreatedAt = order.CreatedAt,
                Items = order.Items.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    OrderId = oi.OrderId,
                    ProductId = oi.ProductId,
                    ProductName = oi.ProductName,
                    Color = oi.Color,
                    UnitPrice = oi.UnitPrice,
                    Quantity = oi.Quantity
                }).ToList()
            }).ToList();
        }
    }
}
