
namespace Labb2Webb.Shared.DTOs
{
    public enum ProductStatus
    {
        In_Stock,
        Out_Of_Stock,
        Discontinued
    }
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public string ProductCategory { get; set; }
        public int Quantity { get; set; }
        public ProductStatus Status { get; set; }
    }
}
