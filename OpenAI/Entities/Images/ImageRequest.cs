using System.Text.Json.Serialization;

namespace Fitomad.OpenAI.Entities.Image;

public record ImageRequest
{
    [JsonPropertyName("prompt")]
    public string Prompt { get; internal set; }
    [JsonPropertyName("model")]
    public string ModelName { get; internal set; }
    [JsonPropertyName("n")]
    public int ImagesCount { get; internal set; }
    [JsonPropertyName("quality")]
    public string Quality { get; internal set; }
    [JsonPropertyName("response_format")]
    public string ResponseFormat { get; internal set; }
    [JsonPropertyName("size")]
    public string Size { get; internal set; }
    [JsonPropertyName("style")]
    public string Style { get; internal set; }
    [JsonPropertyName("user")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? User { get; internal set; }

    internal ImageRequest()
    {
        Prompt = string.Empty;
        ModelName = "dall-e-2";
        ImagesCount = 1;
        Quality = "standard";
        ResponseFormat = "url";
        Size = "1024x1024";
        Style = "vivid";
    }
}