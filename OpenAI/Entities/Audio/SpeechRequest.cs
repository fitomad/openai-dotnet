using System.Text.Json.Serialization;

namespace Fitomad.OpenAI.Entities.Audio;

public record SpeechRequest
{
    public string Model { get; internal set; }
    public string Input { get; internal set; }
    public string Voice { get; internal set; }
    public string ResponseFormat { get; internal set; }
    public double Speed { get; internal set; }

    internal SpeechRequest()
    {
        ResponseFormat = "mp3";
        Speed = 1.0;
    }
}