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
    public async Task<IEnumerable<ProductDto>> SearchProductsByNameAsync(string productName)
    {
        var result = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>($"api/Product/search?name={productName}");
        return result ?? new List<ProductDto>();
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<ProductDto>($"api/Product/{id}");
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/Product/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateProductAsync(ProductDto product)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Product/{product.Id}", product);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateProductAsync(CreateProductDto newProduct)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/Product", newProduct);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> MarkAsDiscontinued(int id)
    {
        var product = await GetProductByIdAsync(id);
        if (product == null)
        {
            return false;
        }
        product.Status = ProductStatus.Discontinued;
        return await UpdateProductAsync(product);
    }

}
