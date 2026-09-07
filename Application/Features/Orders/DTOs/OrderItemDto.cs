namespace Application.Features.Orders.DTOs
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid? ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? Color { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal => UnitPrice * Quantity;
    }
}
