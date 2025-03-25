using System.Net.Http.Headers;
using System.Net.Http.Json;
using Labb2Webb.Shared.DTOs;
using Microsoft.JSInterop;

namespace BlazorFrontend.Services
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public CustomerService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task<CustomerDto> GetLoggedInCustomerProfileAsync()
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
            return await _httpClient.GetFromJsonAsync<CustomerDto>("api/Customer/profile");
        }
        public async Task<HttpResponseMessage> UpdateCustomerAsync(UpdateCustomerDto updateDto)
        {
            return await _httpClient.PutAsJsonAsync($"api/customer/{updateDto.Email}", updateDto);
        }
    }
}
