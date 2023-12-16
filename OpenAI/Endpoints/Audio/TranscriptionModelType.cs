namespace Fitomad.OpenAI.Endpoints.Audio;

public enum TranscriptionModelType
{
    Whisper1
}

public static class TranscriptionModelTypeExtension
{
    public static string GetValue(this TranscriptionModelType model)
    {
        var modelName = model switch
        {
            TranscriptionModelType.Whisper1 => "whisper-1",
            _ => "whisper-1"
        };

        return modelName;
    }
}