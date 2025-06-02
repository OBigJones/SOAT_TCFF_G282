using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repository;
using Application.Services.Products;
using Application.Services.Products.Payload;
using Domain.Entities;
using Domain.Enums;
using Moq;
using Xunit;

namespace Tests.Services.Products
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productService = new ProductService(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllProductsAsync_ReturnsMappedProducts()
        {
            // Arrange
            var entities = new List<ProductEntity>
            {
                new ProductEntity { Id = 1, Name = "Burger", Description = "Desc", Price = 10, Quantity = 5, Type = ProductType.Burger }
            };
            _productRepositoryMock.Setup(r => r.GetAllProductsAsync()).ReturnsAsync(entities);

            // Act
            var result = await _productService.GetAllProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Burger", result.First().Name);
        }

        [Fact]
        public async Task GetAllProductsInStockAsync_ReturnsMappedProducts()
        {
            // Arrange
            var entities = new List<ProductEntity>
            {
                new ProductEntity { Id = 2, Name = "Bebida", Description = "Refri", Price = 5, Quantity = 10, Type = ProductType.Beverage }
            };
            _productRepositoryMock.Setup(r => r.GetProductsInStockAsync()).ReturnsAsync(entities);

            // Act
            var result = await _productService.GetAllProductsInStockAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Bebida", result.First().Name);
        }

        [Fact]
        public async Task GetMenuAsync_ReturnsMenuInfo()
        {
            // Arrange
            var entities = new List<ProductEntity>
            {
                new ProductEntity { Id = 1, Name = "Burger", Description = "Desc", Price = 10, Quantity = 5, Type = ProductType.Burger },
                new ProductEntity { Id = 2, Name = "Bebida", Description = "Refri", Price = 5, Quantity = 10, Type = ProductType.Beverage },
                new ProductEntity { Id = 3, Name = "Sobremesa", Description = "Doce", Price = 7, Quantity = 3, Type = ProductType.Dessert }
            };
            _productRepositoryMock.Setup(r => r.GetAllProductsAsync()).ReturnsAsync(entities);

            // Act
            var result = await _productService.GetMenuAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Burgers);
            Assert.NotNull(result.Beverages);
            Assert.NotNull(result.Desserts);
            Assert.Single(result.Burgers);
            Assert.Single(result.Beverages);
            Assert.Single(result.Desserts);
        }

        [Fact]
        public async Task UpdateProductByIdAsync_NullPayload_ThrowsArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _productService.UpdateProductByIdAsync(null));
        }

        [Fact]
        public async Task UpdateProductByIdAsync_ValidPayload_CallsRepositoryAndReturnsResult()
        {
            // Arrange
            var payload = new ProductPayload
            {
                Id = 1,
                Name = "Burger",
                Description = "Desc",
                Price = 10,
                Stock = 5,
                Type = ProductType.Burger
            };
            _productRepositoryMock.Setup(r => r.UpdateProductAsync(It.IsAny<ProductEntity>())).ReturnsAsync(true);

            // Act
            var result = await _productService.UpdateProductByIdAsync(payload);

            // Assert
            Assert.True(result);
            _productRepositoryMock.Verify(r => r.UpdateProductAsync(It.IsAny<ProductEntity>()), Times.Once);
        }
    }
}