using Application.Services.Order.Payload;
using Application.Services.Order.Response;
using Domain.Enums;

namespace Application.Services.Order
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateOrderAsync(OrderPayload orderDetails);
        Task<IEnumerable<OrderResponse>> GetOrdersByStatusAsync(OrderStatus status);
        Task<bool> UpdateOrderStatusAsync(string orderCode, OrderStatus newStatus);
    }
}
