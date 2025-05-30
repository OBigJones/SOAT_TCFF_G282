using Domain.Entities;

namespace Application.Repository
{
    public interface IOrderRepository
    {
        Task<bool> CreateOrderAsync(OrderEntity order);
        Task<List<OrderEntity>> GetActiveOrders();
        Task<List<OrderEntity>> GetOrdersByStatusAsync(string status);
        Task<bool> UpdateOrderStatusAsync(string orderCode, string newStatus);
    }
}
