using Application.Services.Order;
using Application.Services.Order.Payload;
using Application.Services.Payment;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("payments")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("{orderCode}")]
    public IActionResult GenerateQrCode([FromRoute] string orderCode)
    {
        if (orderCode == null)
        {
            return BadRequest("Order details cannot be null.");
        }
        var result = _paymentService.GenerateQrCode(orderCode);

        return Ok(result);
    }

    [HttpPost("{orderCode}/payed")]
    public IActionResult UpdateOrderStatus([FromBody] string orderCode)
    {
        _paymentService.UpdateStatusOrder(orderCode);
        return Ok("Payment status updated successfully.");
    }
}