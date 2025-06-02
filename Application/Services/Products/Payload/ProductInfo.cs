using Domain.Enums;

namespace Application.Services.Products.Payload
{
    public class ProductInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public ProductType Category { get; set; }
    }
}
