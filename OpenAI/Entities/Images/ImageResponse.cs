using System.Text.Json.Serialization;

namespace Fitomad.OpenAI.Entities.Image;

public record ImageResponse
{
    [JsonPropertyName("created")]
    public int CreationDate { get; init; }
    [JsonPropertyName("data")]
    public UrlImage[] Images { get; init; }


    public record UrlImage
    {
        [JsonPropertyName("url")]
        public string Url { get; init; }
    }
}