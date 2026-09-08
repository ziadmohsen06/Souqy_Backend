using Application.Features.Cart.DTOs;
using Application.Features.Cart.Service;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Souqy.Tests
{
    public class CartTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task AddToCartAsync_ValidItem_CreatesCartAndItem()
        {
            // Arrange
            var context = GetDbContext();
            var service = new CartService(context);

            var categoryId = Guid.NewGuid();
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Test Shirt",
                Price = 29.99m,
                CategoryId = categoryId,
                CreatedAt = DateTime.UtcNow
            };
            var variant = new ProductVariant
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                Color = "Blue",
                StockQuantity = 10
            };

            context.Products.Add(product);
            context.ProductVariants.Add(variant);
            await context.SaveChangesAsync();

            var userId = Guid.NewGuid();

            // Act
            var dto = new AddToCartDto
            {
                ProductId = product.Id,
                ProductVariantId = variant.Id,
                Quantity = 2
            };
            var result = await service.AddToCartAsync(userId, dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Id, result.ProductId);
            Assert.Equal(variant.Id, result.ProductVariantId);
            Assert.Equal("Test Shirt", result.ProductName);
            Assert.Equal("Blue", result.Color);
            Assert.Equal(2, result.Quantity);

            var cart = await service.GetCartAsync(userId);
            Assert.Single(cart);
            Assert.Equal(2, cart[0].Quantity);
        }

        [Fact]
        public async Task AddToCartAsync_InsufficientStock_ThrowsInvalidOperationException()
        {
            // Arrange
            var context = GetDbContext();
            var service = new CartService(context);

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Limited Shoes",
                Price = 99.99m,
                CategoryId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            };
            var variant = new ProductVariant
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                Color = "Red",
                StockQuantity = 1 // Only 1 in stock
            };

            context.Products.Add(product);
            context.ProductVariants.Add(variant);
            await context.SaveChangesAsync();

            var userId = Guid.NewGuid();

            // Act & Assert
            var dto = new AddToCartDto
            {
                ProductId = product.Id,
                ProductVariantId = variant.Id,
                Quantity = 5 // Requesting 5
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => service.AddToCartAsync(userId, dto));
        }

        [Fact]
        public async Task RemoveFromCartAsync_OwnershipCheck_UserCanRemoveOwnItem()
        {
            // Arrange
            var context = GetDbContext();
            var service = new CartService(context);

            var product = new Product { Id = Guid.NewGuid(), Name = "Hat", Price = 15m, CategoryId = Guid.NewGuid(), CreatedAt = DateTime.UtcNow };
            var variant = new ProductVariant { Id = Guid.NewGuid(), ProductId = product.Id, Color = "Black", StockQuantity = 20 };
            context.Products.Add(product);
            context.ProductVariants.Add(variant);
            await context.SaveChangesAsync();

            var userId = Guid.NewGuid();
            var item = await service.AddToCartAsync(userId, new AddToCartDto { ProductId = product.Id, ProductVariantId = variant.Id, Quantity = 1 });

            // Act
            var removed = await service.RemoveFromCartAsync(userId, item.Id);

            // Assert
            Assert.True(removed);
            var cart = await service.GetCartAsync(userId);
            Assert.Empty(cart);
        }

        [Fact]
        public async Task RemoveFromCartAsync_OwnershipCheck_UserCannotRemoveOtherUsersItem()
        {
            // Arrange
            var context = GetDbContext();
            var service = new CartService(context);

            var product = new Product { Id = Guid.NewGuid(), Name = "Jacket", Price = 120m, CategoryId = Guid.NewGuid(), CreatedAt = DateTime.UtcNow };
            var variant = new ProductVariant { Id = Guid.NewGuid(), ProductId = product.Id, Color = "Green", StockQuantity = 10 };
            context.Products.Add(product);
            context.ProductVariants.Add(variant);
            await context.SaveChangesAsync();

            var userA = Guid.NewGuid();
            var userB = Guid.NewGuid();

            // User A adds item to cart
            var itemUserA = await service.AddToCartAsync(userA, new AddToCartDto { ProductId = product.Id, ProductVariantId = variant.Id, Quantity = 1 });

            // Act: User B attempts to remove User A's item
            var removedByB = await service.RemoveFromCartAsync(userB, itemUserA.Id);

            // Assert
            Assert.False(removedByB); // Operation rejected due to ownership check

            // Verify item still exists in User A's cart
            var cartUserA = await service.GetCartAsync(userA);
            Assert.Single(cartUserA);
        }
    }
}
