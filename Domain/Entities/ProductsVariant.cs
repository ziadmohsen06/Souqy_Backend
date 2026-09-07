namespace Domain.Entities;

public class ProductVariant
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public string Size { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;

    public string? ColorImageUrl { get; set; }

    public int StockQuantity { get; set; }

    public Product Product { get; set; } = null!;
}