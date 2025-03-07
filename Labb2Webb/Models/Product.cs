using System.ComponentModel.DataAnnotations;

namespace Labb2Webb.Models
{
    public enum ProductStatus
    {
        In_Stock,
        Out_Of_Stock,
        Discontinued
    }

    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string ProductNumber { get; set; }

        [Required]
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ProductCategory { get; set; }

        [Required]
        public ProductStatus Status { get; set; }

    }
}
