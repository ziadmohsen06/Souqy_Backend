namespace Application.Features.Products.DTOs
{
    public class ColorVariantDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Color { get; set; } = string.Empty;
        public string? ColorImageUrl { get; set; }
        public string Size { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public bool InStock => StockQuantity > 0;
    }
}
