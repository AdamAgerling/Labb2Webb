using Labb2Webb.Shared.DTOs;

namespace Labb2Webb.Shared.Helpers
{
    public class OrderStatusHelper
    {
        public static string GetStatusClass(OrderStatus status)
        {
            return status switch
            {
                OrderStatus.Unhandled => "bg-warning",
                OrderStatus.Packing => "bg-primary",
                OrderStatus.Sent => "bg-info",
                OrderStatus.Delivered => "bg-success",
                _ => "bg-secondary"
            };
        }
    }
}
