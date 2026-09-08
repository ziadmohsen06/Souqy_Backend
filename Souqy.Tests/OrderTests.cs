using Application.Features.Cart.DTOs;
using Application.Features.Cart.Service;
using Application.Features.Orders.DTOs;
using Application.Features.Orders.Service;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace Souqy.Tests
{
    public class OrderTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task CreateOrderAsync_FromCart_CreatesOrderDeductsStockClearsCart()
        {
            // Arrange
            var context = GetDbContext();
            var cartService = new CartService(context, NullLogger<CartService>.Instance);
            var orderService = new OrderService(context, NullLogger<OrderService>.Instance);

            var product = new Product { Id = Guid.NewGuid(), Name = "Watch", Price = 150m, CategoryId = Guid.NewGuid(), CreatedAt = DateTime.UtcNow };
            var variant = new ProductVariant { Id = Guid.NewGuid(), ProductId = product.Id, Color = "Silver", StockQuantity = 10 };
            context.Products.Add(product);
            context.ProductVariants.Add(variant);
            await context.SaveChangesAsync();

            var userId = Guid.NewGuid();
            await cartService.AddToCartAsync(userId, new AddToCartDto { ProductId = product.Id, ProductVariantId = variant.Id, Quantity = 2 });

            // Act
            var createDto = new CreateOrderDto
            {
                ShippingAddress = "123 Main St",
                IdempotencyKey = "TEST-KEY-100"
            };
            var order = await orderService.CreateOrderAsync(userId, createDto);

            // Assert
            Assert.NotNull(order);
            Assert.Equal(userId, order.UserId);
            Assert.Equal("123 Main St", order.ShippingAddress);
            Assert.Equal("TEST-KEY-100", order.IdempotencyKey);
            Assert.Equal(300m, order.TotalAmount); // 150 * 2
            Assert.Single(order.Items);

            // Verify variant stock was deducted from 10 to 8
            var updatedVariant = await context.ProductVariants.FindAsync(variant.Id);
            Assert.NotNull(updatedVariant);
            Assert.Equal(8, updatedVariant.StockQuantity);

            // Verify user's cart was cleared
            var cart = await cartService.GetCartAsync(userId);
            Assert.Empty(cart);
        }

        [Fact]
        public async Task CreateOrderAsync_IdempotencyKey_ReturnsSameOrderOnDuplicateRequest()
        {
            // Arrange
            var context = GetDbContext();
            var cartService = new CartService(context, NullLogger<CartService>.Instance);
            var orderService = new OrderService(context, NullLogger<OrderService>.Instance);

            var product = new Product { Id = Guid.NewGuid(), Name = "Backpack", Price = 45m, CategoryId = Guid.NewGuid(), CreatedAt = DateTime.UtcNow };
            var variant = new ProductVariant { Id = Guid.NewGuid(), ProductId = product.Id, Color = "Grey", StockQuantity = 5 };
            context.Products.Add(product);
            context.ProductVariants.Add(variant);
            await context.SaveChangesAsync();

            var userId = Guid.NewGuid();
            await cartService.AddToCartAsync(userId, new AddToCartDto { ProductId = product.Id, ProductVariantId = variant.Id, Quantity = 1 });

            var createDto = new CreateOrderDto
            {
                ShippingAddress = "456 Oak Ave",
                IdempotencyKey = "IDEMPOTENCY-KEY-UNIQUE-1"
            };

            // Act: First creation call
            var order1 = await orderService.CreateOrderAsync(userId, createDto);

            // Act: Duplicate creation call with same idempotency key
            var order2 = await orderService.CreateOrderAsync(userId, createDto);

            // Assert
            Assert.NotNull(order1);
            Assert.NotNull(order2);
            Assert.Equal(order1.Id, order2.Id); // Same order ID returned
            Assert.Equal(order1.IdempotencyKey, order2.IdempotencyKey);

            // Verify stock was deducted only ONCE (from 5 to 4)
            var updatedVariant = await context.ProductVariants.FindAsync(variant.Id);
            Assert.NotNull(updatedVariant);
            Assert.Equal(4, updatedVariant.StockQuantity);
        }

        [Fact]
        public async Task GetOrderAsync_OwnershipCheck_UserCannotRetrieveOtherUsersOrder()
        {
            // Arrange
            var context = GetDbContext();
            var cartService = new CartService(context, NullLogger<CartService>.Instance);
            var orderService = new OrderService(context, NullLogger<OrderService>.Instance);

            var product = new Product { Id = Guid.NewGuid(), Name = "Sunglasses", Price = 75m, CategoryId = Guid.NewGuid(), CreatedAt = DateTime.UtcNow };
            var variant = new ProductVariant { Id = Guid.NewGuid(), ProductId = product.Id, Color = "Black", StockQuantity = 10 };
            context.Products.Add(product);
            context.ProductVariants.Add(variant);
            await context.SaveChangesAsync();

            var userA = Guid.NewGuid();
            var userB = Guid.NewGuid();

            await cartService.AddToCartAsync(userA, new AddToCartDto { ProductId = product.Id, ProductVariantId = variant.Id, Quantity = 1 });
            var orderA = await orderService.CreateOrderAsync(userA, new CreateOrderDto { ShippingAddress = "User A Address" });

            // Act: User B tries to get User A's order
            var resultForUserB = await orderService.GetOrderAsync(userB, orderA.Id);

            // Act: User A gets User A's order
            var resultForUserA = await orderService.GetOrderAsync(userA, orderA.Id);

            // Assert
            Assert.Null(resultForUserB); // User B gets null (ownership check enforced)
            Assert.NotNull(resultForUserA); // User A gets their order
            Assert.Equal(orderA.Id, resultForUserA.Id);
        }
    }
}
