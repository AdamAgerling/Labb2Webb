using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Labb2Webb.DTOs;
using Labb2Webb.Models;
using Labb2Webb.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace Labb2Webb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;



        public AuthController(IConfiguration configuration, ICustomerRepository customerRepository, IMapper mapper)
        {
            _configuration = configuration;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet("email/{email}", Name = "GetCustomerByEmail")]
        public async Task<ActionResult<Customer>> GetCustomerByEmail(string email)
        {
            var customer = await _customerRepository.GetByEmailAsync(email);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateCustomerDto createCustomerDto)
        {
            if (createCustomerDto == null)
            {
                return BadRequest("Invalid registration data");
            }

            var customer = _mapper.Map<Customer>(createCustomerDto);
            customer.Email = createCustomerDto.Email.ToLower();

            customer.Role = Role.User;

            var passwordHasher = new PasswordHasher<Customer>();
            customer.PasswordHash = passwordHasher.HashPassword(customer, createCustomerDto.Password);

            await _customerRepository.AddCustomerAsync(customer);

            var cusomerDto = _mapper.Map<CustomerDto>(customer);

            return CreatedAtAction("GetCustomerByEmail", new { email = customer.Email }, cusomerDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var customer = await _customerRepository.GetByEmailAsync(login.Email);
            if (customer == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            var passwordHasher = new PasswordHasher<Customer>();
            var result = passwordHasher.VerifyHashedPassword(customer, customer.PasswordHash, login.Password);


            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid credentials.");
            }

            var token = GenerateJwtToken(customer);
            return Ok(new { token });
        }

        private string GenerateJwtToken(Customer customer)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, customer.Email),
                new Claim(ClaimTypes.Role, customer.Role.ToString())
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}


