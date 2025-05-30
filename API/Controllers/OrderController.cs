using Application.Services.Order;
using Application.Services.Order.Payload;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("CreateOrder")]
        public IActionResult CreateOrder([FromBody] OrderPayload orderDetails)
        {
            if (orderDetails == null)
            {
                return BadRequest("Order details cannot be null.");
            }
            var result = _orderService.CreateOrderAsync(orderDetails);

            return Ok(result);
        }

        [HttpGet("GetOrderByStatus")]
        public IActionResult GetOrderByStatus([FromQuery] string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                return BadRequest("Status cannot be null or empty.");
            }
            var orders = _orderService.GetOrdersByStatusAsync(status);
            if (orders == null || !orders.Result.Any())
            {
                return NotFound("No orders found with the specified status.");
            }
            return Ok(orders);
        }

        [HttpPost("UpdateOrderStatus")]
        public IActionResult UpdateOrderStatus([FromBody] string orderCode, string status)
        {
            if (string.IsNullOrEmpty(status) || string.IsNullOrEmpty(orderCode))
            {
                return BadRequest("Order code and status cannot be null or empty.");
            }
            var result = _orderService.UpdateOrderStatusAsync(orderCode, status);
            if (!result.Result)
            {
                return NotFound("Order not found or status update failed.");
            }
            return Ok("Order status updated successfully.");
        }
    }
}
