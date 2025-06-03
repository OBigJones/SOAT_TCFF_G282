using System.Text.Json.Serialization;

namespace Infra.Pag.Request;

public class Identification
{
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("number")]
    public string Number { get; set; }
}