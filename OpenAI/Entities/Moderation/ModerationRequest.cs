using System.Text.Json.Serialization;

namespace Fitomad.OpenAI.Entities.Moderation;

public record ModerationRequest
{
    [JsonPropertyName("input")]
    public string Input { get; internal set; } = string.Empty;
    [JsonPropertyName("model")]
    public string Model { get; internal set; } = " text-moderation-latest";
}