namespace Labb2Webb.DTOs
{
    public enum OrderStatus
    {
        Unhandled,
        Packing,
        Sent,
        Delivered
    }
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Unhandled;

        public List<OrderItemDto> OrderItems { get; set; }
    }
}
