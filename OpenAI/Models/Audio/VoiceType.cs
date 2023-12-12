namespace Fitomad.OpenAI.Models.Audio;

public enum VoiceType
{
    Alloy,
    Echo,
    Fable,
    Onyx,
    Nova,
    Shimmer
}

public static class VoiceTypeExtension
{
    public static string GetValue(this VoiceType voice)
    {
        var voiceName = voice switch
        {
            VoiceType.Alloy => "alloy",
            VoiceType.Echo => "echo",
            VoiceType.Fable => "fable",
            VoiceType.Onyx => "onyx",
            VoiceType.Nova => "nova",
            VoiceType.Shimmer => "shimmer",
            _ => "alloy"
        };

        return voiceName;
    }
}