using System.Text.Json.Serialization;

namespace Fitomad.OpenAI.Entities.Audio;

public record SpeechRequest
{
    [JsonPropertyName("model")]
    public string Model { get; set; }
    [JsonPropertyName("input")]
    public string Input { get; internal set; }
    [JsonPropertyName("voice")]
    public string Voice { get; internal set; }
    [JsonPropertyName("response_format")]
    public string ResponseFormat { get; internal set; }
    [JsonPropertyName("speed")]
    public double Speed { get; internal set; }

    internal SpeechRequest()
    {
        ResponseFormat = "mp3";
        Speed = 1.0;
    }
}