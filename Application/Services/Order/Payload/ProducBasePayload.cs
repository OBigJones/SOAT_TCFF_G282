namespace Application.Services.Order.Payload;

public class ProducBasePayload
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}