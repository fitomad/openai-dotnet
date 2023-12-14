using System.Text;
using System.Text.Json;
using System.Net.Http.Json;
using Fitomad.OpenAI.Entities.Chat;

namespace Fitomad.OpenAI.Endpoints.Chat;

public interface IChatEndpoint
{
    public Task<ChatResponse> CreateChatAsync(ChatRequest chatRequest);
    public Task<ChatResponse> ExplainImageAsync(string userInput, string imageUrl, int tokensCount);
}

public sealed class ChatEndpoint: IChatEndpoint
{
    private HttpClient _httpClient;

    internal ChatEndpoint(HttpClient httpClient)
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

    public async Task<ChatResponse> ExplainImageAsync(string imageUrl, string userQuestion, int tokensCount = 300)
    {
        var imageExplanationRequest = new ImageExplanationRequest
        {
            ModelName = ChatModelType.GPT_4_VISION_PREVIEW.GetValue(),
            TokensCount = tokensCount,
            Messages = [
                new ImageExplanationRequest.Message
                {
                    Role = "user",
                    Content = [
                        new ImageExplanationRequest.Content
                        {
                            Type = "text",
                            Text = userQuestion
                        },
                        new ImageExplanationRequest.Content
                        {
                            Type = "image-url",
                            ImageUrl = new ImageExplanationRequest.ImageUrl
                            {
                                Url = imageUrl
                            }
                        }
                    ]
                }
            ]
        };

        var payload = JsonSerializer.Serialize(imageExplanationRequest);

        var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync(Endpoint.Create, httpContent);

        var imageExplanationResponse = await response.Content.ReadFromJsonAsync<ChatResponse>();

        return imageExplanationResponse;
    }

    internal static class Endpoint
    {
        internal const string Create = "v1/chat/completions";
    }
}
