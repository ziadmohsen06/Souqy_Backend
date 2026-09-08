using System.ComponentModel.DataAnnotations;

namespace Application.Features.Cart.DTOs
{
    public class AddToCartDto
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Guid ProductVariantId { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; } = 1;
    }
}
