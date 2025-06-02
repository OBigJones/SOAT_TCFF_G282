using System.Text.Json.Serialization;

namespace Infra.Pag.Response;

public class PointOfInteraction
{
    [JsonPropertyName("transaction_data")]
    public TransactionData TransactionData { get; set; }
}