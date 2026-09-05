using Mapster;
using Souqy.Features.Products.Domain;

namespace Souqy.Features.Products
{
    public class ProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<ProductDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default)
        {
            var all = (await _repository.GetAllAsync(ct)).OrderBy(p => p.CreatedAt).ToList();
            var total = all.Count;
            var items = all.Skip((page - 1) * pageSize).Take(pageSize)
                .Select(p => p.Adapt<ProductDto>());

            return new PagedResult<ProductDto>
            {
                Page = page,
                PageSize = pageSize,
                Total = total,
                Items = items
            };
        }

        public async Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var p = await _repository.GetByIdAsync(id, ct);
            return p is null ? null : p.Adapt<ProductDto>();
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto input, CancellationToken ct = default)
        {
            var product = input.Adapt<Product>();
            product.Id = Guid.NewGuid();
            product.CreatedAt = DateTime.UtcNow;

            await _repository.AddAsync(product, ct);
            return product.Adapt<ProductDto>();
        }

        public async Task UpdateAsync(Guid id, UpdateProductDto update, CancellationToken ct = default)
        {
            var existing = await _repository.GetByIdAsync(id, ct);
            if (existing is null) throw new InvalidOperationException("Product not found");

            // Map updated fields onto the existing product instance
            update.Adapt(existing);

            await _repository.UpdateAsync(existing, ct);
        }

        public Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            return _repository.DeleteAsync(id, ct);
        }
    }
}
