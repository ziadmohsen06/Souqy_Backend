using Application.Features.Products.DTOs;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Services
{
    public class ProductVariantService
    {
        private readonly ApplicationDbContext _context;

        public ProductVariantService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductVariantDto>> GetVariantsAsync(Guid productId, CancellationToken ct = default)
        {
            var productExists = await _context.Products.AnyAsync(p => p.Id == productId, ct);
            if (!productExists)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            var variants = await _context.ProductVariants
                .AsNoTracking()
                .Where(v => v.ProductId == productId)
                .OrderBy(v => v.Color)
                .ToListAsync(ct);

            return variants.Select(MapToDto).ToList();
        }

        public async Task<ProductVariantDto> CreateVariantAsync(Guid productId, CreateProductVariantDto dto, CancellationToken ct = default)
        {
            var productExists = await _context.Products.AnyAsync(p => p.Id == productId, ct);
            if (!productExists)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            var color = dto.Color.Trim();
            var duplicate = await _context.ProductVariants
                .AnyAsync(v => v.ProductId == productId && v.Color.ToLower() == color.ToLower(), ct);
            if (duplicate)
            {
                throw new InvalidOperationException($"A variant with color '{color}' already exists for this product.");
            }

            var variant = new ProductVariant
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                Size = dto.Size.Trim(),
                Color = color,
                ColorImageUrl = string.IsNullOrWhiteSpace(dto.ColorImageUrl) ? null : dto.ColorImageUrl.Trim(),
                StockQuantity = dto.StockQuantity
            };

            _context.ProductVariants.Add(variant);
            await _context.SaveChangesAsync(ct);

            return MapToDto(variant);
        }

        public async Task<bool> DeleteVariantAsync(Guid productId, Guid variantId, CancellationToken ct = default)
        {
            var variant = await _context.ProductVariants
                .FirstOrDefaultAsync(v => v.Id == variantId && v.ProductId == productId, ct);

            if (variant == null)
            {
                return false;
            }

            _context.ProductVariants.Remove(variant);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        private static ProductVariantDto MapToDto(ProductVariant v) => new()
        {
            Id = v.Id,
            ProductId = v.ProductId,
            Size = v.Size,
            Color = v.Color,
            ColorImageUrl = v.ColorImageUrl,
            StockQuantity = v.StockQuantity
        };
    }
}
