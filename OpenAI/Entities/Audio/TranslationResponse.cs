using System.Text.Json.Serialization;

namespace Fitomad.OpenAI.Entities.Audio;

public record TranslationResponse
{
    [JsonPropertyName("text")]
    public string Text { get; init; }
}