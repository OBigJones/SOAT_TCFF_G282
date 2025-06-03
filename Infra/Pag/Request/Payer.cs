using System.Text.Json.Serialization;

namespace Infra.Pag.Request;

public class Payer
{
    [JsonPropertyName("entity_type")]
    public string EntityType { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("identification")]
    public Identification Identification { get; set; }
}