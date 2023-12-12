namespace Fitomad.OpenAI.Entities.Audio;

public record TranscriptionResponse
{
    public string Text { get; internal init; }
}