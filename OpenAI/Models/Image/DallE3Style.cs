namespace OpenAI.Models.Image;

public enum DallE3Style
{
    Vivid,
    Natural
}

public static class DallE3StyleExtension
{
    public static string GetValue(this DallE3Style style)
    {
        var styleName = style switch
        {
            DallE3Style.Vivid => "vivid",
            DallE3Style.Natural => "natural",
            _ => "natural"
        };

        return styleName;
    }
}