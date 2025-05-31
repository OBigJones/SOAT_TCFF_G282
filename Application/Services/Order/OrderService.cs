using Application.Repository;
using Application.Services.Order.Mappers;
using Application.Services.Order.Payload;
using Application.Services.Order.Response;

namespace Application.Services.Order
{
    public class OrderService(IOrderRepository orderRepository) : IOrderService
    {
        public async Task<OrderResponse> CreateOrderAsync(OrderPayload orderDetails)
        {
            if (orderDetails == null)
            {
                throw new ArgumentNullException(nameof(orderDetails), "Order details cannot be null.");
            }

            var orderEntity = OrderMapper.ToEntity(orderDetails);
            orderEntity.CalculateTotalPrice();

            var result = orderRepository.CreateOrderAsync(orderEntity);
            if (!result.Result)
            {
                throw new Exception("Failed to create order.");
            }

            return OrderMapper.ToResponse(orderEntity);
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersByStatusAsync(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                throw new ArgumentException("Status cannot be null or empty.", nameof(status));
            }
            var orders = await orderRepository.GetOrdersByStatusAsync(status);
            if (orders == null || !orders.Any())
            {
                return Enumerable.Empty<OrderResponse>();
            }
            // return _mapper.Map<IEnumerable<OrderResponse>>(orders);
            return null;
        }

        public async Task<bool> UpdateOrderStatusAsync(string orderCode, string newStatus)
        {
            if (string.IsNullOrEmpty(newStatus))
            {
                throw new ArgumentException("New status cannot be null or empty.", nameof(newStatus));
            }
            return await orderRepository.UpdateOrderStatusAsync(orderCode, newStatus);
        }
    }
}
