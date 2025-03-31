using System.ComponentModel.DataAnnotations;

namespace Labb2Webb.Shared.DTOs
{
    public class CreateProductDto
    {
        [Required]
        public string ProductNumber { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }
        [Required]
        public string ProductCategory { get; set; }
    }
}
