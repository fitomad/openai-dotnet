using System.Text.Json.Serialization;

namespace Fitomad.OpenAI.Entities.Audio;

public record SpeechResponse
{
    public string ResponseFormat { get; internal init; }
    public byte[] Content { get; internal init; }

    public void SaveToFile(string filePath)
    {
        File.WriteAllBytes(filePath, Content);
    }
}