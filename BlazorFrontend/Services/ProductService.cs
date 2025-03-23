using System.Net.Http.Json;
using Labb2Webb.Shared.DTOs;

public class ProductService
{
    private readonly HttpClient _httpClient;
    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/Product");
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(string category)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>($"api/Product/category/{category}");
    }

    public async Task<ProductDto> GetProductByNameAsync(string productNumber, string productName)
    {
        return await _httpClient.GetFromJsonAsync<ProductDto>($"api/Product/{productNumber}/{productName}");
    }
}
