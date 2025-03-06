using System.ComponentModel.DataAnnotations;

namespace Labb2Webb.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public string Address { get; set; }
    }
}
