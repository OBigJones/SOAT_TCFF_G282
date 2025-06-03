using Domain.Enums;

namespace Application.Services.Order.Payload;

public class ProductBasePayload
{
    public long ProductId { get; set; }
    public string ProductName { get; set; }
}