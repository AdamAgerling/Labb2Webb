using System.ComponentModel.DataAnnotations;

namespace Labb2Webb.Shared
{
    public class UpdateCustomerDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? Cellphone { get; set; }
        public string? Address { get; set; }
    }
}
