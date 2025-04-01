namespace Labb2Webb.Models
{
    public enum OrderStatus
    {
        Unhandled,
        Packing,
        Sent,
        Delivered
    }
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string CustomerEmail { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public OrderStatus Status { get; set; } = OrderStatus.Unhandled;

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
