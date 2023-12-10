using System.Text;
using System.Text.Json;
using System.Net.Http.Json;
using OpenAI.Entities.Chat;
using System.Net.Http.Headers;

namespace OpenAI.Models.Chat;

public sealed class ChatModel
{
    private HttpClient _httpClient;

    internal ChatModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ChatResponse> CreateChatAsync(ChatRequest chatRequest)
    {
        var payload = JsonSerializer.Serialize(chatRequest);
        var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync(Endpoint.Create, httpContent);

        var chatResponse = await response.Content.ReadFromJsonAsync<ChatResponse>();

        return chatResponse;
    }

    internal static class Endpoint
    {
        internal const string Create = "v1/chat/completions";
    }
}
