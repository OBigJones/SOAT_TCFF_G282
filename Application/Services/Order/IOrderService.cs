using Application.Services.Order.Payload;
using Application.Services.Order.Response;

namespace Application.Services.Order
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateOrderAsync(OrderPayload orderDetails);
        Task<IEnumerable<OrderResponse>> GetOrdersByStatusAsync(string status);
        Task<bool> UpdateOrderStatusAsync(string orderCode, string newStatus);
    }
}
