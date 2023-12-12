using System.Runtime.CompilerServices;

namespace Fitomad.OpenAI.Models.Chat;

public enum ChatModelType
{
    GPT_4,
    GPT_4_1106_PREVIEW,
    GPT_4_VISION_PREVIEW,
    GPT_4_32K,
    GPT_3_5_TURBO,
    GPT_3_5_TURBO_16K
}

public static class ChatModelKindExtension
{
    public static string GetValue(this ChatModelType model)
    {
        var modelName = model switch
        {
            ChatModelType.GPT_4 => "gpt-4",
            ChatModelType.GPT_4_1106_PREVIEW => "gpt-4-1106-preview",
            ChatModelType.GPT_4_VISION_PREVIEW => "gpt-4-vision-preview",
            ChatModelType.GPT_4_32K => "gpt-4-32k",
            ChatModelType.GPT_3_5_TURBO => "gpt-3.5-turbo",
            ChatModelType.GPT_3_5_TURBO_16K => "gpt-3.5-turbo-16k",
            _ => "gpt-3.5-turbo"
        };

        return modelName;
    }
}