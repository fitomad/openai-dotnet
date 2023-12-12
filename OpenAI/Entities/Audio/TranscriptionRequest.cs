namespace Fitomad.OpenAI.Entities.Audio;

public record TranscriptionRequest
{
    public string File { get; internal set; }
    public string Model { get; internal set; }
    public string Language { get; internal set; }
    public string Prompt { get; internal set; }
    public string ResponseFormat { get; internal set; }
    public double Temperatute { get; internal set; }

    internal TranscriptionRequest()
    {
        ResponseFormat = "json";
        Temperatute = 0.0;
    }
}