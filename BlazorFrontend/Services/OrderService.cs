using System.Net.Http.Json;
using Labb2Webb.Shared.DTOs;
using Labb2Webb.Shared.Enums;

namespace BlazorFrontend.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<OrderDto>>("api/Order") ?? new List<OrderDto>();
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus status)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/order/{orderId}/status", status);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerEmailAsync(string email)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<OrderDto>>($"api/Order/customer?email={email}")
                ?? new List<OrderDto>();
        }
    }
}
