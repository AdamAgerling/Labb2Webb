using System.Net.Http.Json;
using Labb2Webb.Shared.DTOs;

namespace BlazorFrontend.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerEmailAsync(string email)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<OrderDto>>($"api/Order/customer?email={email}")
                ?? new List<OrderDto>();
        }
    }
}
