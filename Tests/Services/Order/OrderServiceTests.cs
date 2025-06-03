using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repository;
using Application.Services.Order;
using Application.Services.Order.Payload;
using Application.Services.Order.Response;
using Domain.Enums;
using Domain.Entities;
using Moq;
using Xunit;

namespace Tests.Services.Order
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _orderService = new OrderService(_orderRepositoryMock.Object, _userRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateOrderAsync_NullPayload_ThrowsArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _orderService.CreateOrderAsync(null));
        }

        [Fact]
        public async Task CreateOrderAsync_RepositoryReturnsFalse_ThrowsException()
        {
            // Arrange
            var payload = new OrderPayload
            {
                CustomerName = "Cliente Teste",
                BurgerList = new List<ProductBasePayload>
                {
                    new ProductBasePayload { ProductId = 1, ProductName = "Burger" }
                },
                Status = OrderStatus.Received
            };

            // O mapeamento real n�o � testado aqui, pois OrderMapper � est�tico.
            _orderRepositoryMock
                .Setup(r => r.CreateOrderAsync(It.IsAny<OrderEntity>()))
                .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _orderService.CreateOrderAsync(payload));
        }

        [Fact]
        public async Task GetOrdersByStatusAsync_NoOrders_ReturnsEmpty()
        {
            // Arrange
            _orderRepositoryMock
                .Setup(r => r.GetOrdersByStatusAsync(It.IsAny<OrderStatus>()))
                .ReturnsAsync(new List<OrderEntity>());

            // Act
            var result = await _orderService.GetOrdersByStatusAsync(OrderStatus.Received);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task UpdateOrderStatusAsync_RepositoryReturnsTrue_ReturnsTrue()
        {
            // Arrange
            var orderCode = "ORDER123";
            var newStatus = OrderStatus.Ready;

            _orderRepositoryMock
                .Setup(r => r.UpdateOrderStatusAsync(orderCode, newStatus))
                .ReturnsAsync(true);

            // Act
            var result = await _orderService.UpdateOrderStatusAsync(orderCode, newStatus);

            // Assert
            Assert.True(result);
            _orderRepositoryMock.Verify(r => r.UpdateOrderStatusAsync(orderCode, newStatus), Times.Once);
        }
    }
}   