using System.Text.Json.Serialization;

namespace Infra.Pag.Response;

public class TransactionData
{
    [JsonPropertyName("qr_code")]
    public string QrCode { get; set; }
}