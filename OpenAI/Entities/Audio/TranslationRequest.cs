using System.Text.Json.Serialization;

namespace Fitomad.OpenAI.Entities.Audio;

public record TranslationRequest
{
    [JsonPropertyName("file")]
    public string File { get; internal set; }
    [JsonPropertyName("model")]
    public string Model { get; internal set; }
    [JsonPropertyName("language")]
    public string Language { get; internal set; }
    [JsonPropertyName("prompt")]
    public string Prompt { get; internal set; }
    [JsonPropertyName("response_format")]
    public string ResponseFormat { get; internal set; }
    [JsonPropertyName("temperature")]
    public double Temperatute { get; internal set; }

    public string FileName
    {
        get => Path.GetFileName(File);
    }

    public string TemperatureStringValue {
        get => Temperatute.ToString();
    }
}