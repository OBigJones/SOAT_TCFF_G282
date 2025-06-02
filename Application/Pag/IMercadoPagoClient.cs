using Domain.Entities;

namespace Application.Pag;

public interface IMercadoPagoClient
{
    Task<string> CreatePaymentAsync(OrderEntity order);
}