using Domain.Entities;

namespace Infrastructure.Products
{
    public class InMemoryCategoryRepository : ICategoryRepository
    {
        private readonly List<Category> _items;

        public InMemoryCategoryRepository()
        {
                _items = new List<Category>
            {
                new Category { Id = Guid.NewGuid(), Name = "Default", Description = "Default category" }
            };
        }

        public Task<IEnumerable<Category>> GetAllAsync(CancellationToken ct = default)
        {
            return Task.FromResult<IEnumerable<Category>>(_items.ToList());
        }

        public Task<Category?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return Task.FromResult(_items.FirstOrDefault(x => x.Id == id));
        }
    }
}
