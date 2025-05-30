using Domain.Entities;

namespace Application.Services.Order.Payload
{
    public class OrderPayload
    {
        public string CustomerName { get; set; }
        public List<ProductBase> BurgerList { get; set; }
        public List<ProductBase> Beverages { get; set; }
        public List<ProductBase> Desserts { get; set; }
    }
}
