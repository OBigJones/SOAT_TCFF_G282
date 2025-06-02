using System.Text.Json.Serialization;

namespace Infra.Pag.Response;

public class PaymentResponse
{
    [JsonPropertyName("point_of_interaction")]
    public PointOfInteraction PointOfInteraction { get; set; }
}