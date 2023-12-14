namespace Fitomad.OpenAI.Endpoints.Image;

public enum ImageResponseFormat
{
    Url,
    B64Json
}

public static class ImageResponseFormatExtension
{
    public static string GetValue(this ImageResponseFormat format)
    {
        var formatName = format switch
        {
            ImageResponseFormat.Url => "url",
            ImageResponseFormat.B64Json => "b64_json",
            _ => "url"
        };

        return formatName;
    }
}