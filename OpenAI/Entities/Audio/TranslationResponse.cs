using System.Text.Json.Serialization;

namespace Fitomad.OpenAI.Entities.Audio;

public record TranslationResponse
{
    public string Text { get; init; }
}