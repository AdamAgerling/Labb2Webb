﻿using Labb2Webb.Models;

namespace Labb2Webb.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> SearchProductsByNameAsync(string name);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string name);
        Task<Product> GetByProductNumberAsync(string productNumber);
        Task<Product> GetProductByNumberAndNameAsync(string productNumber, string productName);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}
