using System;

namespace Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductVariantId { get; set; }
        
        // Snapshot fields from Product and ProductVariant at time of adding to cart
        public string ProductName { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string? ColorImageUrl { get; set; }
        public decimal UnitPrice { get; set; }
        
        public int Quantity { get; set; }

        // Navigation
        public Cart? Cart { get; set; }
        public Product? Product { get; set; }
        public ProductVariant? ProductVariant { get; set; }
    }
}
