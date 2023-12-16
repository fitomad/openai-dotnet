using System.Text.Json.Serialization;

namespace Fitomad.OpenAI.Entities.Models;

public record ModelDeletedResponse
{
    [JsonPropertyName("id")]
    public string ModelId { get; init; }
    [JsonPropertyName("object")]
    public string ObjectType { get; init; }
    [JsonPropertyName("deleted")]
    public bool IsDeleted { get; init; }
}