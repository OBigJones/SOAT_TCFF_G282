using Application.Pag;
using Application.Repository;
using Application.Services.Order;
using Application.Services.Products;
using Domain.Enums;

namespace Application.Services.Payment;

public class PaymentService(IOrderRepository orderRepository, IMercadoPagoClient mercadoPagoClient, IOrderService orderService, IProductRepository productRepository) : IPaymentService
{
    public async Task<string> GenerateQrCode(string orderCode)
    {
        var order = await orderRepository.GetOrderByCode(orderCode);
        if (order == null)
        {
            throw new Exception("Order not found!");
        }

        var paymentAsync = await mercadoPagoClient.CreatePaymentAsync(order);
        return paymentAsync;
    }

    public async Task<bool> UpdateStatusOrder(string orderCode)
    {
        var order = await orderRepository.GetOrderByCode(orderCode);
        if (order == null)
        {
            throw new Exception("Order not found!");
        }

        var result = productRepository.DecrementStockAsync(order.OrderItems.Select(x => x.Product).ToList());
        if (result == null || !result.Result)
        {
            throw new Exception("Decrement product error!");
        }
        
        return await orderService.UpdateOrderStatusAsync(orderCode, OrderStatus.Received);
    }
}