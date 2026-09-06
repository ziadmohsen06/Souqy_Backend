using Application.Features.Categories.DTOs;
using Domain.Entities;
using Infrastructure.Products;
using Mapster;

namespace Application.Features.Categories.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken ct = default)
        {
            var all = await _repository.GetAllAsync(ct);
            return all.Select(c => c.Adapt<CategoryDto>());
        }

        public async Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var c = await _repository.GetByIdAsync(id, ct);
            return c is null ? null : c.Adapt<CategoryDto>();
        }
    }
}
