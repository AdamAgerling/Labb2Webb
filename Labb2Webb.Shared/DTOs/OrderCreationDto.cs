namespace Labb2Webb.Shared.DTOs
{
    public class OrderCreationDto
    {
        public string CustomerEmail { get; set; }
        public List<OrderItemCreationDto> OrderItems { get; set; }
    }
}
