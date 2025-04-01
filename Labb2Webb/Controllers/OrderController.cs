using AutoMapper;
using Labb2Webb.Models;
using Labb2Webb.Repositories;
using Labb2Webb.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Labb2Webb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly ECommerceContext _context;

        public OrderController(IOrderRepository orderRepository, IMapper mapper, ICustomerRepository customerRepository, ECommerceContext context)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _customerRepository = customerRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            var orderDto = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return Ok(orderDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            var orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);
        }

        [HttpGet("customer")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByCustomerEmail([FromQuery] string email)
        {
            var orders = await _orderRepository.GetOrdersByCustomerEmailAsync(email);

            if (orders == null)
            {
                return NotFound("No orders found for this customer.");
            }
            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return Ok(orderDtos);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] OrderCreationDto orderDto)
        {
            var customer = await _customerRepository.GetByEmailAsync(orderDto.CustomerEmail);

            if (customer == null)
            {
                return NotFound("The customer was not found!");
            }

            var order = new Order
            {
                CustomerEmail = orderDto.CustomerEmail,
                CustomerId = customer.Id,
                OrderDate = DateTime.UtcNow,
                OrderItems = orderDto.OrderItems.Select(oi => new OrderItem
                {
                    ProductId = oi.ProductId,
                    Amount = oi.Quantity,
                }).ToList()
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Ok(order.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            if (id != order.Id)
            {
                return BadRequest("The Order-Id does not match.");
            }

            await _orderRepository.UpdateOrderAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderRepository.DeleteOrderAsync(id);
            return NoContent();
        }

        //[HttpPut("{id}/status")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> UpdateStatus(int id, [FromBody] OrderStatus newStatus)
        //{
        //    var order = await _orderRepository.GetOrderByIdAsync(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    order.Status = newStatus;
        //    await _orderRepository.UpdateOrderAsync(order);
        //    return NoContent();
        //}

        [HttpPost("{orderId}/items")]
        public async Task<IActionResult> AddOrderItem(int orderId, [FromBody] OrderItem orderItem)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound("The order could not be found");
            }

            order.OrderItems.Add(orderItem);

            await _orderRepository.UpdateOrderAsync(order);

            return NoContent();
        }
    }
}

