﻿using System.ComponentModel.DataAnnotations;

namespace Labb2Webb.Shared.DTOs
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

        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string? VerifyNewPassword { get; set; }
    }
}
