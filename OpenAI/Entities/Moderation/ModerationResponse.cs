using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Fitomad.OpenAI.Entities.Moderation;

public record ModerationResponse
{
    [JsonPropertyName("id")]
    public string ModerationID { get; init; }
    [JsonPropertyName("model")]
    public string Model { get; init; }
    [JsonPropertyName("results")]
    public ModerationResult[] Results { get; init; }

    public record ModerationResult
    {
        [JsonPropertyName("flagged")]
        public bool Flagged { get; init; }
        [JsonPropertyName("categories")]
        public CategoryValues Values { get; init; }
        [JsonPropertyName("category_scores")]
        public CategoryScores Scores { get; init; }
    }

    public record CategoryValues
    {
        [JsonPropertyName("sexual")]
        public bool Sexual { get; init; }
        [JsonPropertyName("hate")]
        public bool Hate { get; init; }
        [JsonPropertyName("harassment")]
        public bool Harassment { get; init; }
        [JsonPropertyName("self-harm")]
        public bool SelfHarm { get; init; }
        [JsonPropertyName("sexual/minors")]
        public bool SexualMinors { get; init; }
        [JsonPropertyName("hate/threatening")]
        public bool HateThreatening { get; init; }
        [JsonPropertyName("violence/graphic")]
        public bool ViolenceGraphic { get; init; }
        [JsonPropertyName("self-harm/intent")]
        public bool SelfHarmIntent { get; init; }
        [JsonPropertyName("self-harm/instructions")]
        public bool SelfHarmInstructions { get; init; }
        [JsonPropertyName("harassment/threatening")]
        public bool HarassmentThreatening { get; init; }
        [JsonPropertyName("violence")]
        public bool Violence { get; init; }
    }

    public record CategoryScores
    {
        [JsonPropertyName("sexual")]
        public double Sexual { get; init; }
        [JsonPropertyName("hate")]
        public double Hate { get; init; }
        [JsonPropertyName("harassment")]
        public double Harassment { get; init; }
        [JsonPropertyName("self-harm")]
        public double SelfHarm { get; init; }
        [JsonPropertyName("sexual/minors")]
        public double SexualMinors { get; init; }
        [JsonPropertyName("hate/threatening")]
        public double HateThreatening { get; init; }
        [JsonPropertyName("violence/graphic")]
        public double ViolenceGraphic { get; init; }
        [JsonPropertyName("self-harm/intent")]
        public double SelfHarmIntent { get; init; }
        [JsonPropertyName("self-harm/instructions")]
        public double SelfHarmInstructions { get; init; }
        [JsonPropertyName("harassment/threatening")]
        public double HarassmentThreatening { get; init; }
        [JsonPropertyName("violence")]
        public double Violence { get; init; }
    }
}