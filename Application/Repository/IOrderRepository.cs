using Domain.Entities;
using Domain.Enums;

namespace Application.Repository
{
    public interface IOrderRepository
    {
        Task<bool> CreateOrderAsync(OrderEntity order);
        Task<List<OrderEntity>> GetActiveOrders();
        Task<List<OrderEntity>> GetOrdersByStatusAsync(OrderStatus status);
        Task<bool> UpdateOrderStatusAsync(string orderCode, OrderStatus newStatus);
    }
}
