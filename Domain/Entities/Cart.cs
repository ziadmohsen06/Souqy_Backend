using System;

namespace Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation
        public User? User { get; set; }
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
