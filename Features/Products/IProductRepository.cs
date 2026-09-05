using Souqy.Features.Products.Domain;

namespace Souqy.Features.Products
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(CancellationToken ct = default);
        Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(Product product, CancellationToken ct = default);
        Task UpdateAsync(Product product, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
    }
}
