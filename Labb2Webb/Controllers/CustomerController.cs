using AutoMapper;
using Labb2Webb.Models;
using Labb2Webb.Repositories;
using Labb2Webb.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Labb2Webb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, IOrderRepository orderRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("email")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Customer>> GetCustomerByEmail(string email)
        {
            var customer = await _customerRepository.GetByEmailAsync(email);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<CustomerDto>> GetMyProfile()
        {
            var email = User.Identity.Name;
            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized();
            }

            var customer = await _customerRepository.GetByEmailAsync(email);
            if (customer == null)
            {
                return NotFound();
            }

            var customerDto = _mapper.Map<CustomerDto>(customer);
            return Ok(customerDto);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            await _customerRepository.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomerByEmail), new { email = customer.Email }, customer);

        }

        [HttpPut("{email}")]
        [Authorize]
        public async Task<IActionResult> UpdateCustomer(string email, [FromBody] UpdateCustomerDto updateDto)
        {
            if (!email.Equals(updateDto.Email, StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("The email in the URL does not match the email in the request body.");
            }

            if (!User.IsInRole("Admin"))
            {
                var currentUserEmail = User.Identity.Name;
                if (!email.Equals(currentUserEmail, StringComparison.OrdinalIgnoreCase))
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You are not allowed to update another user's account.");
                }
            }

            var existingCustomer = await _customerRepository.GetByEmailAsync(email);
            if (existingCustomer == null)
            {
                return NotFound();
            }


            _mapper.Map(updateDto, existingCustomer);

            if (!string.IsNullOrWhiteSpace(updateDto.NewPassword))
            {
                var passwordHasher = new PasswordHasher<Customer>();
                existingCustomer.PasswordHash = passwordHasher.HashPassword(existingCustomer, updateDto.NewPassword);
            }

            await _customerRepository.UpdateCustomerAsync(existingCustomer);
            return NoContent();
        }

        [HttpDelete("{email}")]
        [Authorize]
        public async Task<IActionResult> DeleteCustomer(string email)
        {
            if (!User.IsInRole("Admin"))
            {
                var currentUserEmail = User.Identity.Name;
                if (!email.Equals(currentUserEmail, StringComparison.OrdinalIgnoreCase))
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You are not allowed to delete another user's account.");
                }
            }
            await _customerRepository.DeleteCustomerByEmailAsync(email);
            return NoContent();
        }
    }
}

