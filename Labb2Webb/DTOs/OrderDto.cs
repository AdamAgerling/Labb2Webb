﻿namespace Labb2Webb.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }
    }
}
