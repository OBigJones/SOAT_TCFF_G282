using Application.Repository;
using Application.Services.Order.Mappers;
using Application.Services.Order.Payload;
using Application.Services.Order.Response;
using Domain.Enums;

namespace Application.Services.Order
{
    public class OrderService(IOrderRepository orderRepository, IUserRepository userRepository, IProductRepository productRepository) : IOrderService
    {
        public async Task<OrderResponse> CreateOrderAsync(OrderPayload orderDetails)
        {
            if (orderDetails == null)
            {
                throw new ArgumentNullException(nameof(orderDetails), "Order details cannot be null.");
            }

            var orderEntity = OrderMapper.ToEntity(orderDetails);
            var user = await userRepository.IdentificationAsync(orderDetails.CustomerCpf);
            if(user != null) 
                orderEntity.User = user;
            
            foreach (var item in orderEntity.OrderItems)
            {
                var product = productRepository.GetProductByIdAsync(item.ProductId);
                if(product.Result == null) 
                    throw new ArgumentNullException(nameof(item), "Product not found.");
                
                item.Product = product.Result;
            }

            orderEntity.Status = OrderStatus.Created;
            orderEntity.CalculateTotalPrice();

            var result = await orderRepository.CreateOrderAsync(orderEntity);
            if (!result)
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
