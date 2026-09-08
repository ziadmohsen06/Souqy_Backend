namespace Application.Features.Orders.DTOs
{
    public class CreateOrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateOrderDto
    {
        public string ShippingAddress { get; set; } = string.Empty;
        public string? IdempotencyKey { get; set; }
        public List<CreateOrderItemDto>? Items { get; set; }
    }
}
