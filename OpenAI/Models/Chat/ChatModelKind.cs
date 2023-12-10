using System.Runtime.CompilerServices;

namespace Fitomad.OpenAI.Models.Chat;

public enum ChatModelKind
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
    public static string GetValue(this ChatModelKind model)
    {
        var modelName = model switch
        {
            ChatModelKind.GPT_4 => "gpt-4",
            ChatModelKind.GPT_4_1106_PREVIEW => "gpt-4-1106-preview",
            ChatModelKind.GPT_4_VISION_PREVIEW => "gpt-4-vision-preview",
            ChatModelKind.GPT_4_32K => "gpt-4-32k",
            ChatModelKind.GPT_3_5_TURBO => "gpt-3.5-turbo",
            ChatModelKind.GPT_3_5_TURBO_16K => "gpt-3.5-turbo-16k",
            _ => "gpt-3.5-turbo"
        };

        return modelName;
    }
}