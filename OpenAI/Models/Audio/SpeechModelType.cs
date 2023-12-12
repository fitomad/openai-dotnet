namespace Fitomad.OpenAI.Models.Audio;

public enum SpeechModelType
{
    TTS_1,
    TTS_1_HD
}

public static class SpeechModelTypeExtension
{
    public static string GetValue(this SpeechModelType model)
    {
        var modelName = model switch
        {
            SpeechModelType.TTS_1 => "tts-1",
            SpeechModelType.TTS_1_HD => "tts-1-hd",
            _ => "tts-1"
        };

        return modelName;
    }
}