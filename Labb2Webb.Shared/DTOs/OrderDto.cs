﻿namespace Labb2Webb.Shared.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }
    }
}
