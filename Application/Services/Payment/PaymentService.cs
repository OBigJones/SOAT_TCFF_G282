using Application.Pag;
using Application.Repository;
using Application.Services.Order;
using Domain.Enums;

namespace Application.Services.Payment;

public class PaymentService(IOrderRepository orderRepository, IMercadoPagoClient mercadoPagoClient, IOrderService orderService) : IPaymentService
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
        return await orderService.UpdateOrderStatusAsync(orderCode, OrderStatus.Received);
    }
}