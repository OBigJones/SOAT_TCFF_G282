namespace Application.Services.Payment;

public interface IPaymentService
{
    Task<string> GenerateQrCode(string orderCode);
    Task<bool> UpdateStatusOrder(string orderCode);
}