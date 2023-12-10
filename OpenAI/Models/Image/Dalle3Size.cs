namespace OpenAI.Models.Image;

public enum DallE3Size
{
    Square,
    Portrait,
    Landscape
}

public static class DallE3SizeExtension
{
    public static string GetValue(this DallE3Size size)
    {
        var sizeName = size switch
        {
            DallE3Size.Landscape => "1024x1792",
            DallE3Size.Portrait => "1792x1024",
            DallE3Size.Square => "1024x1024",
            _ => "1024x1024"
        };

        return sizeName;
    }
}