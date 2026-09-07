using Mapster;
using Domain.Entities;
using Infrastructure.Products;
using Infrastructure.Services;
using Application.Features.Products.DTOs;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Application.Features.Products.Services
{
    public class ProductService
    {
        private readonly IEmbeddingService _embeddingService;
        private readonly IProductRepository _repository;
        private readonly Microsoft.Extensions.Caching.Memory.IMemoryCache _cache;

        public ProductService(IProductRepository repository, Microsoft.Extensions.Caching.Memory.IMemoryCache cache, IEmbeddingService embeddingService)
        {
            _repository = repository;
            _cache = cache;
            _embeddingService = embeddingService;
        }

        public async Task<PagedResult<ProductDto>> GetPagedAsync(int page, int pageSize, Guid? categoryId = null, CancellationToken ct = default)
        {
            var cacheKey = $"products:page={page}:pageSize={pageSize}:category={categoryId?.ToString() ?? "all"}";

            if (_cache.TryGetValue(cacheKey, out object? cachedObj))
            {
                if (cachedObj is PagedResult<ProductDto> cached)
                    return cached;
            }

            var all = (await _repository.GetAllAsync(ct)).OrderBy(p => p.CreatedAt).ToList();

            if (categoryId.HasValue)
            {
                all = all.Where(p => p.CategoryId == categoryId.Value).ToList();
            }
            var total = all.Count;
            var items = all.Skip((page - 1) * pageSize).Take(pageSize)
                .Select(p => p.Adapt<ProductDto>());
            var result = new PagedResult<ProductDto>
            {
                Page = page,
                PageSize = pageSize,
                Total = total,
                Items = items
            };

            // Cache for 60 seconds using CreateEntry
            var entry = _cache.CreateEntry(cacheKey);
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
            entry.Value = result;
            entry.Dispose();

            return result;
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

            // KNOWN GAP: a newly created product has zero ProductVariants. Stock and
            // per-color images now live on ProductVariant, so until a variant is added
            // (separate endpoint, not yet built) this product cannot be added to a cart
            // or ordered. Acceptable for now: variants are managed independently.
            // 1. Get the float array from Python (gracefully handles Python service being down)
            float[]? embeddingArray = await _embeddingService.GenerateEmbeddingAsync($"{product.Name}. {product.Description}");

            // 2. Convert it to a string format PostgreSQL understands, or leave null if failed
            if (embeddingArray != null && embeddingArray.Length > 0)
            {
                product.Embedding = "[" + string.Join(",", embeddingArray) + "]";
            }
            else
            {
                product.Embedding = null; // Save without embedding if Python is unavailable
            }

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



        public async Task<IEnumerable<RecommendationDto>> GetRecommendationsAsync(Guid productId, int count = 4, CancellationToken ct = default)
        {
            // 1. Get the raw JSON string from Infrastructure
            var jsonString = await _embeddingService.GetRecommendationsJsonAsync(productId, count);
            
            // 2. Deserialize it here in Application where the DTO lives
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<RecommendationDto>>(jsonString, options) ?? new List<RecommendationDto>();
        }
    }
}
