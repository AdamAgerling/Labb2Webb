using System.ComponentModel.DataAnnotations;

namespace Labb2Webb.Models
{
    public enum Role
    {
        User,
        Admin
    }
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

        public string? Cellphone { get; set; }

        public string? Address { get; set; }


        public string PasswordHash { get; set; }

        public Role Role { get; set; }

        public decimal GetAdminDiscount()
        {
            return Role == Role.Admin ? 0.2m : 0.0m;
        }
    }
}
