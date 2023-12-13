namespace Fitomad.OpenAI.Models.Moderation;

public enum ModerationModelType
{
    TextModerationStable,
    TextModerationLatest
}

public static class ModerationModelTypeExtension
{
    public static string GetValue(this ModerationModelType model)
    {
        var modelName = model switch
        {
            ModerationModelType.TextModerationStable => "text-moderation-stable",
            ModerationModelType.TextModerationLatest => "text-moderation-latest",
            _ => "text-moderation-latest"
        };

        return modelName;
    }
}