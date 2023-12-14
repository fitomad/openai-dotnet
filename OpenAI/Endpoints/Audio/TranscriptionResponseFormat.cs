namespace Fitomad.OpenAI.Endpoints.Audio;

public enum TranscriptionResponseFormat
{
    Json,
    Text,
    STR,
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
            TranscriptionResponseFormat.STR => "str",
            TranscriptionResponseFormat.VerboseJson => "verbose_json",
            TranscriptionResponseFormat.VTT => "vtt",
            _ => "json"
        };

        return modelName;
    }
}