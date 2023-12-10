namespace Fitomad.OpenAI.Models.Image;

public enum DallE3Quality
{
    HD,
    Standard
}

public static class DallE3QualityExtension
{
    public static string GetValue(this DallE3Quality quality)
    {
        var qualityName = quality switch
        {
            DallE3Quality.HD => "hd",
            DallE3Quality.Standard => "standard",
            _ => "standard"
        };

        return qualityName; 
    }
}