using Application.Pag;
using Application.Repository;
using Application.Services.Order;
using Domain.Enums;

namespace Application.Services.Payment;

public class PaymentService : IPaymentService
{
    private readonly IMercadoPagoClient _mercadoPagoClient;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderService _orderService;
    
    public PaymentService(IMercadoPagoClient mercadoPagoClient, IOrderRepository orderRepository, IOrderService orderService)
    {
        _mercadoPagoClient = mercadoPagoClient;
        _orderRepository = orderRepository;
        _orderService = orderService;
    }


    public async Task<string> GenerateQrCode(string orderCode)
    {
        var order = await _orderRepository.GetOrderByCode(orderCode);
        if (order == null)
        {
            throw new Exception("Order not found!");
        }

        var paymentAsync = _mercadoPagoClient.CreatePaymentAsync(order);
        return paymentAsync.Result;
    }

    public async Task UpdateStatusOrder(string orderCode)
    {
        await _orderService.UpdateOrderStatusAsync(orderCode, OrderStatus.Received);
    }
}