using System.Text;
using System.Text.Json;
using System.Net.Http.Json;

using Fitomad.OpenAI.Entities.Moderation;

namespace Fitomad.OpenAI.Models.Moderation;

public interface IModerationModel
{
    public Task<ModerationResponse> CreateModeration(ModerationRequest request);
}

public sealed class ModerationModel: IModerationModel
{
    private HttpClient _httpClient;

    internal ModerationModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ModerationResponse> CreateModeration(ModerationRequest request)
    {
        var payload = JsonSerializer.Serialize(request);
        Console.WriteLine(payload);
        var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync(Endpoint.Create, httpContent);
        
        var moderationResponse = await response.Content.ReadFromJsonAsync<ModerationResponse>();

        return moderationResponse;
    }

    internal static class Endpoint
    {
        internal const string Create = "v1/moderations";
    }
}
