using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Labb2Webb.DTOs
{
    public class CreateCustomerDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; } = string.Empty;
        [Required]
        [PasswordPropertyText]
        [Compare("Password")]
        public string VerifyPassword { get; set; } = string.Empty;

    }
}
