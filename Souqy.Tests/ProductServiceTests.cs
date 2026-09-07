using Application.Features.Products.Services;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Products;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Souqy.Tests
{
    public class DummyEmbeddingService : IEmbeddingService
    {
        public Task<float[]?> GenerateEmbeddingAsync(string text)
        {
            return Task.FromResult<float[]?>(new float[] { 0.1f, 0.2f, 0.3f });
        }

        public Task<string> GetRecommendationsJsonAsync(Guid productId, int count = 4)
        {
            return Task.FromResult("[]");
        }
    }

    public class ProductServiceTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task GetPagedAsync_AppliesPaginationAndCaching()
        {
            // Arrange
            var context = GetDbContext();
            var repo = new EfProductRepository(context);
            var cache = new MemoryCache(new MemoryCacheOptions());
            var embeddingService = new DummyEmbeddingService();
            var service = new ProductService(repo, cache, embeddingService);

            var categoryId = Guid.NewGuid();
            for (int i = 1; i <= 25; i++)
            {
                context.Products.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = $"Product {i}",
                    Price = 10m * i,
                    CategoryId = categoryId,
                    CreatedAt = DateTime.UtcNow.AddMinutes(i)
                });
            }
            await context.SaveChangesAsync();

            // Act: Request page 1 with 10 items
            var page1 = await service.GetPagedAsync(page: 1, pageSize: 10, categoryId: categoryId);

            // Assert
            Assert.NotNull(page1);
            Assert.Equal(25, page1.Total);
            Assert.Equal(10, page1.Items.Count());

            // Act: Request page 1 again (should hit cache)
            var cachedPage1 = await service.GetPagedAsync(page: 1, pageSize: 10, categoryId: categoryId);

            // Assert
            Assert.Equal(page1.Total, cachedPage1.Total);
            Assert.Equal(page1.Items.Count(), cachedPage1.Items.Count());
        }

        [Fact]
        public async Task GetPagedAsync_FiltersByCategory()
        {
            // Arrange
            var context = GetDbContext();
            var repo = new EfProductRepository(context);
            var cache = new MemoryCache(new MemoryCacheOptions());
            var embeddingService = new DummyEmbeddingService();
            var service = new ProductService(repo, cache, embeddingService);

            var catA = Guid.NewGuid();
            var catB = Guid.NewGuid();

            context.Products.Add(new Product { Id = Guid.NewGuid(), Name = "Item A1", Price = 10m, CategoryId = catA, CreatedAt = DateTime.UtcNow });
            context.Products.Add(new Product { Id = Guid.NewGuid(), Name = "Item A2", Price = 20m, CategoryId = catA, CreatedAt = DateTime.UtcNow });
            context.Products.Add(new Product { Id = Guid.NewGuid(), Name = "Item B1", Price = 30m, CategoryId = catB, CreatedAt = DateTime.UtcNow });
            await context.SaveChangesAsync();

            // Act
            var resultCatA = await service.GetPagedAsync(page: 1, pageSize: 10, categoryId: catA);
            var resultCatB = await service.GetPagedAsync(page: 1, pageSize: 10, categoryId: catB);

            // Assert
            Assert.Equal(2, resultCatA.Total);
            Assert.Equal(1, resultCatB.Total);
        }
    }
}
