using Application.Repository;
using Application.Services.Order.Mappers;
using Application.Services.Order.Payload;
using Application.Services.Order.Response;
using Domain.Enums;

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
            orderEntity.Status = OrderStatus.Received;
            orderEntity.CalculateTotalPrice();

            var result = orderRepository.CreateOrderAsync(orderEntity);
            if (!result.Result)
            {
                throw new Exception("Failed to create order.");
            }

            return OrderMapper.ToResponse(orderEntity);
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersByStatusAsync(OrderStatus status)
        {
            var orders = await orderRepository.GetOrdersByStatusAsync(status);
            if (orders == null || !orders.Any())
            {
                return Enumerable.Empty<OrderResponse>();
            }
            return OrderMapper.ToResponseList(orders);
        }

        public async Task<bool> UpdateOrderStatusAsync(string orderCode, OrderStatus newStatus)
        {
            return await orderRepository.UpdateOrderStatusAsync(orderCode, newStatus);
        }
    }
}
