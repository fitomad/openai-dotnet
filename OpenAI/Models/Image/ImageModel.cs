using System.Text;
using System.Text.Json;
using System.Net.Http.Json;

using OpenAI.Entities.Image;

namespace OpenAI.Models.Image;

public sealed class ImageModel
{
    private HttpClient _httpClient;

    internal ImageModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ImageResponse> CreateImageAsync(ImageRequest request)
    {
        string jsonRequest = JsonSerializer.Serialize(request);
        StringContent payload = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync(Endpoint.Create, payload);
        var imageResponse = await response.Content.ReadFromJsonAsync<ImageResponse>();

        return imageResponse;
    }

    internal static class Endpoint
    {
        internal const string Create = "v1/images/generations";
        internal const string Edit = "v1/images/edits";
        internal const string Variation = "v1/images/variations";
    }
}
