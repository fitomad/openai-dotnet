using System.Numerics;

namespace Fitomad.OpenAI.Models.Image;

public enum ImageModelKind
{
    DALL_E_2,
    DALL_E_3
}

public static class ImageModelKindExtension
{
    public static string GetValue(this ImageModelKind kind)
    {
        var modelName = kind switch
        {
            ImageModelKind.DALL_E_2 => "dall-e-2",
            ImageModelKind.DALL_E_3 => "dall-e-3",
            _ => "dall-e-2"
        };

        return modelName;
    }
}