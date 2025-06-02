namespace Application.Services.Payment;

public interface IPaymentService
{
    Task<string> GenerateQrCode(string orderCode);
    Task UpdateStatusOrder(string orderCode);
}