using System.Text.Json.Serialization;

namespace Fitomad.OpenAI.Entities.Models;

public record ModelResponse
{
    [JsonPropertyName("id")]
    public string ModelId { get; init; }
    [JsonPropertyName("object")]
    public string ObjectType { get; init; }
    [JsonPropertyName("created")]
    public int CreatedDate { get; init; }
    [JsonPropertyName("owned_by")]
    public string Owner { get; init; }
}