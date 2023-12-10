namespace Fitomad.OpenAI.Models.Image;

public enum DallE2Size
{
    Large,
    Medium,
    Small
}

public static class DallE2SizeExtension
{
    public static string GetValue(this DallE2Size size)
    {
        var sizeName = size switch
        {
            DallE2Size.Large => "1024x1024",
            DallE2Size.Medium => "512x512",
            DallE2Size.Small => "256x256",
            _ => "512x512"
        };

        return sizeName;
    }
}