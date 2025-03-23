using Labb2Webb.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb2Webb.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceContext _context;

        public ProductRepository(ECommerceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return new List<Product>();
            }

            return await _context.Products
                .Where(p => p.ProductCategory.ToLower() == category.ToLower())
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> SearchProductsByNameAsync(string name)
        {
            return await _context.Products.Where(p => p.ProductName.Contains(name)).ToListAsync();
        }

        public async Task<Product> GetByProductNumberAsync(string productNumber)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductNumber == productNumber);
        }

        public async Task<Product> GetProductByNumberAndNameAsync(string productNumber, string productName)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.ProductNumber == productNumber && p.ProductName == productName);
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
