using System.Text.Json.Serialization;

namespace Infra.Pag.Request;

public class Item
{
    [JsonPropertyName("id")]
    public string Id { get; set; } // Assumindo que order.Id seja um int

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("picture_url")]
    public string PictureUrl { get; set; }

    [JsonPropertyName("category_id")]
    public string CategoryId { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; } // Assumindo que seja 1

    [JsonPropertyName("unit_price")]
    public decimal UnitPrice { get; set; } // Assumindo que order.TotalPrice seja decimal

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("event_date")]
    public string EventDate { get; set; }

    [JsonPropertyName("warranty")]
    public bool Warranty { get; set; }
}