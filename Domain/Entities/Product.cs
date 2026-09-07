using System;

namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Color { get; set; }
        public string? ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }

        public string? Embedding { get; set; }

        // Navigation
        public Category? Category { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
    
    }
}
