using System.Numerics;

namespace Fitomad.OpenAI.Endpoints.Image;

public enum ImageModelType
{
    DALL_E_2,
    DALL_E_3
}

public static class ImageModelKindExtension
{
    public static string GetValue(this ImageModelType kind)
    {
        var modelName = kind switch
        {
            ImageModelType.DALL_E_2 => "dall-e-2",
            ImageModelType.DALL_E_3 => "dall-e-3",
            _ => "dall-e-2"
        };

        return modelName;
    }
}