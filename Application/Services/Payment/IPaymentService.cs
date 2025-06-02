namespace Application.Services.Payment;

public interface IPaymentService
{
    Task<string> GenerateQrCode(string orderCode);
    void UpdateStatusOrder(string orderCode);
}