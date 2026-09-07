namespace Application.Features.Products.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? DefaultColor { get; set; }
        public string? DefaultImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ColorVariantDto> ColorVariants { get; set; } = new();
    }

    public class CreateProductDto
    {
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(200, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.StringLength(2000)]
        public string? Description { get; set; }

        [System.ComponentModel.DataAnnotations.Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    }

    public class UpdateProductDto
    {
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(200, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.StringLength(2000)]
        public string? Description { get; set; }

        [System.ComponentModel.DataAnnotations.Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
