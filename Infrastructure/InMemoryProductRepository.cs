using Domain.Entities;

namespace Infrastructure.Products
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly List<Product> _items;

        public InMemoryProductRepository()
        {
            _items = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Sample Product 1", Description = "A demo product", Price = 9.99m, CreatedAt = DateTime.UtcNow },
                new Product { Id = Guid.NewGuid(), Name = "Sample Product 2", Description = "Another demo", Price = 19.50m, CreatedAt = DateTime.UtcNow }
            };
        }

        public Task AddAsync(Product product, CancellationToken ct = default)
        {
            _items.Add(product);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var existing = _items.FirstOrDefault(x => x.Id == id);
            if (existing != null) _items.Remove(existing);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Product>> GetAllAsync(CancellationToken ct = default)
        {
            return Task.FromResult<IEnumerable<Product>>(_items.ToList());
        }

        public Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return Task.FromResult(_items.FirstOrDefault(x => x.Id == id));
        }

        public Task UpdateAsync(Product product, CancellationToken ct = default)
        {
            var idx = _items.FindIndex(x => x.Id == product.Id);
            if (idx >= 0) _items[idx] = product;
            return Task.CompletedTask;
        }

        // this method is to satisfy the interface requirement as this file is a dummy in-memory repository only for testing purposes.
        public Task<IEnumerable<Product>> GetRecommendationsAsync(Guid productId, int count = 4, CancellationToken ct = default)
        {
            // Just return an empty list since this is an in-memory dummy repository
            return Task.FromResult<IEnumerable<Product>>(new List<Product>());
        }

    }
}
