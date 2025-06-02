using Domain.Entities;
using Domain.Enums;

namespace Application.Services.Order.Payload
{
    public class OrderPayload
    {
        public long Id { get; set; }
        public string? CustomerName { get; set; }
        public string OrderCode { get; init; }
        public List<ProductBasePayload> BurgerList { get; set; }
        public List<ProductBasePayload> Beverages { get; set; }
        public List<ProductBasePayload> Desserts { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Expiration { get; init; }
    }
}
