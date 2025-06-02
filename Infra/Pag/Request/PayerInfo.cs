using System.Text.Json.Serialization;

namespace Infra.Pag.Request;

public class PayerInfo
{
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }
}