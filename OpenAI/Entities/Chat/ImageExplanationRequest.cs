using System.Text.Json.Serialization;

namespace Fitomad.OpenAI.Entities.Chat;

public record ImageExplanationRequest
{
    [JsonPropertyName("model")]
    public string ModelName { get; internal set; }
    [JsonPropertyName("messages")]
    public Message[] Messages { get; internal set; }
    [JsonPropertyName("max_tokens")]
    public int TokensCount { get; internal set; }

    public record Message
    {
        [JsonPropertyName("role")]
        public string Role { get; internal set; }
        [JsonPropertyName("content")]
        public Content[] Content { get; internal set; }
    }

    public record Content
    {
        [JsonPropertyName("type")]
        public string Type { get; internal set; } 
        [JsonPropertyName("text")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Text { get; internal set; }
        [JsonPropertyName("image_url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ImageUrl? ImageUrl { get; internal set; }
    }

    public record ImageUrl
    {
        [JsonPropertyName("url")]
        public string Url { get; internal set; }
    }
}