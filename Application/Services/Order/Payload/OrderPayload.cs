using Domain.Entities;

namespace Application.Services.Order.Payload
{
    public class OrderPayload
    {
        public long Id { get; set; }
        public string? CustomerName { get; set; }
        public string OrderCode { get; init; }
        public List<ProducBasePayload> BurgerList { get; set; }
        public List<ProducBasePayload> Beverages { get; set; }
        public List<ProducBasePayload> Desserts { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } // e.g., "Pending", "Completed", "Cancelled"
        public DateTime Expiration { get; init; }
    }
}
