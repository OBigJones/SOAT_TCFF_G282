using Domain.Entities;
using Domain.Enums;

namespace Application.Services.Order.Payload
{
    public class OrderPayload
    {
        public string? CustomerName { get; set; }
        public string? CustomerCpf { get; set; }
        public List<ProductBasePayload> BurgerList { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Expiration { get; init; }
    }
}
