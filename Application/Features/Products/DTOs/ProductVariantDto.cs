using System.ComponentModel.DataAnnotations;

namespace Application.Features.Products.DTOs
{
    public class ProductVariantDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Size { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string? ColorImageUrl { get; set; }
        public int StockQuantity { get; set; }
        public bool InStock => StockQuantity > 0;
    }

    public class CreateProductVariantDto
    {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Size { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Color { get; set; } = string.Empty;

        [StringLength(500)]
        public string? ColorImageUrl { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }
    }
}
