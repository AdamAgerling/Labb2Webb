using Labb2Webb.Models;
using Labb2Webb.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Labb2Webb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet("email")]
        public async Task<ActionResult<Customer>> GetCustomerByEmail([FromQuery] string email)
        {
            var customer = await _customerRepository.GetByEmailAsync(email);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            await _customerRepository.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest("Kund-ID matchar inte.");
            }

            await _customerRepository.UpdateCustomerAsync(customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerRepository.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}

