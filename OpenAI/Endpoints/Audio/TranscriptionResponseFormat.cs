namespace Fitomad.OpenAI.Endpoints.Audio;

public enum TranscriptionResponseFormat
{
    Json,
    Text,
    SRT,
    VerboseJson,
    VTT
}

public static class TranscriptionResponseFormatExtension
{
    public static string GetValue(this TranscriptionResponseFormat model)
    {
        var modelName = model switch
        {
            TranscriptionResponseFormat.Json => "json",
            TranscriptionResponseFormat.Text => "text",
            TranscriptionResponseFormat.SRT => "srt",
            TranscriptionResponseFormat.VerboseJson => "verbose_json",
            TranscriptionResponseFormat.VTT => "vtt",
            _ => "json"
        };

        return modelName;
    }
}