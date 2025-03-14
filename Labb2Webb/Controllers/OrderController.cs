using AutoMapper;
using Labb2Webb.DTOs;
using Labb2Webb.Models;
using Labb2Webb.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Labb2Webb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
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

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] OrderDto orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest();
            }

            var order = _mapper.Map<Order>(orderDto);
            await _orderRepository.AddOrderAsync(order);

            var createdOrderDto = _mapper.Map<OrderDto>(order);

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, createdOrderDto);
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

