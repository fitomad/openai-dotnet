using System.Text.Json.Serialization;

namespace Fitomad.OpenAI.Entities.Audio;

public record TranscriptionResponse
{
    [JsonPropertyName("text")]
    public string Text { get; init; }
}