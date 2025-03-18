namespace Labb2Webb.Shared
{
    public class CreateProductDto
    {
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public string ProductCategory { get; set; }
    }
}
