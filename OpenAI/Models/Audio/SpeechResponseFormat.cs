namespace Fitomad.OpenAI.Models.Audio;

public enum SpeechResponseFormat
{
    MP3,
    OPUS,
    AAC,
    FLAC
}

public static class SpeechResponseFormatExtension
{
    public static string GetValue(this SpeechResponseFormat model)
    {
        var modelName = model switch
        {
            SpeechResponseFormat.MP3 => "mp3",
            SpeechResponseFormat.OPUS => "opus",
            SpeechResponseFormat.AAC => "aac",
            SpeechResponseFormat.FLAC => "flac",
            _ => "mp3"
        };

        return modelName;
    }
}