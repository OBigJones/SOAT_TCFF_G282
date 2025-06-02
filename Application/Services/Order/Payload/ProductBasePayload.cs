using Domain.Enums;

namespace Application.Services.Order.Payload;

public class ProductBasePayload
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public ProductType Type { get; set; }
}