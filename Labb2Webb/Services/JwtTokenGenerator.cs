using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Labb2Webb.Models;
using Microsoft.IdentityModel.Tokens;

namespace Labb2Webb.Services
{
    public class JwtTokenGenerator
    {
        private readonly IConfiguration _configuration;
        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(Customer customer)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var firstName = string.IsNullOrWhiteSpace(customer.FirstName) ? "" : customer.FirstName;
            var lastName = string.IsNullOrWhiteSpace(customer.LastName) ? "" : customer.LastName;
            var fullName = (firstName + " " + lastName).Trim();

            if (string.IsNullOrEmpty(fullName))
            {
                fullName = customer.Email;
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, customer.Role.ToString()),
                new Claim(ClaimTypes.Name, customer.Email),
                new Claim("FullName", fullName)
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
