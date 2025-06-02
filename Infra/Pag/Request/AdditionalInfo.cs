using System.Text.Json.Serialization;

namespace Infra.Pag.Request;

public class AdditionalInfo
{
    [JsonPropertyName("items")]
    public List<Item> Items { get; set; }

    [JsonPropertyName("payer")]
    public PayerInfo Payer { get; set; }
}