namespace Application.Services.Payment;

public class PaymentService : IPaymentService
{
    public Task<string> GenerateQrCode(string orderCode)
    {
        throw new NotImplementedException();
    }

    public void UpdateStatusOrder(string orderCode)
    {
        throw new NotImplementedException();
    }
}