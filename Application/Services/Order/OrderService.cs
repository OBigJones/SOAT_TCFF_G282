using Application.Repository;
using Application.Services.Order.Payload;
using Application.Services.Order.Response;
using AutoMapper;
using Domain.Entities;

namespace Application.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        public OrderService(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<OrderResponse> CreateOrderAsync(OrderPayload orderDetails)
        {
            if (orderDetails == null)
            {
                throw new ArgumentNullException(nameof(orderDetails), "Order details cannot be null.");
            }

            var orderEntity = _mapper.Map<OrderEntity>(orderDetails);
            orderEntity.CalculateTotalPrice();

            var result = _orderRepository.CreateOrderAsync(orderEntity);
            if (!result.Result)
            {
                throw new Exception("Failed to create order.");
            }

            return _mapper.Map<OrderResponse>(orderEntity);
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersByStatusAsync(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                throw new ArgumentException("Status cannot be null or empty.", nameof(status));
            }
            var orders = await _orderRepository.GetOrdersByStatusAsync(status);
            if (orders == null || !orders.Any())
            {
                return Enumerable.Empty<OrderResponse>();
            }
            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }

        public async Task<bool> UpdateOrderStatusAsync(string orderCode, string newStatus)
        {
            if (string.IsNullOrEmpty(newStatus))
            {
                throw new ArgumentException("New status cannot be null or empty.", nameof(newStatus));
            }
            return await _orderRepository.UpdateOrderStatusAsync(orderCode, newStatus);
        }
    }
}
