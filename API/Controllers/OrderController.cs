using Application.Services.Order;
using Application.Services.Order.Payload;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderPayload orderDetails)
        {
            if (orderDetails == null)
            {
                return BadRequest("Order details cannot be null.");
            }
            var result = _orderService.CreateOrderAsync(orderDetails);

            return Ok(result.Result);
        }

        [HttpGet("status")]
        public IActionResult GetOrderByStatus([FromQuery] OrderStatus status)
        {
            var orders = _orderService.GetOrdersByStatusAsync(status);
            if (orders == null || !orders.Result.Any())
            {
                return NotFound("No orders found with the specified status.");
            }
            return Ok(orders.Result);
        }

        [HttpPut("{orderCode}")]
        public IActionResult UpdateOrderStatus([FromRoute] string orderCode, [FromBody] OrderStatus status)
        {
            var result = _orderService.UpdateOrderStatusAsync(orderCode, status);
            if (!result.Result)
            {
                return NotFound("Order not found or status update failed.");
            }
            return Ok("Order status updated successfully.");
        }
    }
}
