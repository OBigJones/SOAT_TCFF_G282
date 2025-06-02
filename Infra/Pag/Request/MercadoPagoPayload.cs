using System.Text.Json.Serialization;

namespace Infra.Pag.Request;

public class MercadoPagoPayload
{
    [JsonPropertyName("additional_info")]
    public AdditionalInfo AdditionalInfo { get; set; }

    [JsonPropertyName("binary_mode")]
    public bool BinaryMode { get; set; }

    [JsonPropertyName("capture")]
    public bool Capture { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("external_reference")]
    public string ExternalReference { get; set; }

    [JsonPropertyName("installments")]
    public int Installments { get; set; }

    [JsonPropertyName("payment_method_id")]
    public string PaymentMethodId { get; set; }

    [JsonPropertyName("statement_descriptor")]
    public string StatementDescriptor { get; set; }

    [JsonPropertyName("transaction_amount")]
    public decimal TransactionAmount { get; set; }
    
}