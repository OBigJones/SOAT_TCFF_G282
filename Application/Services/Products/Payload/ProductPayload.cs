using Domain.Enums;

namespace Application.Services.Products.Payload
{
    public class ProductPayload
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public ProductType Type { get; set; }
    }
}
