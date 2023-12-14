using Fitomad.OpenAI.Endpoints.Audio;
using Fitomad.OpenAI.Endpoints.Chat;
using Fitomad.OpenAI.Endpoints.Image;
using Fitomad.OpenAI.Endpoints.Moderation;

namespace Fitomad.OpenAI;

public interface IOpenAIClient
{
    public ChatEndpoint ChatCompletion { get; }
    public ImageEndpoint Image { get; }
    public AudioEndpoint Audio { get; }
    public ModerationEndpoint Moderation { get; }
}

public class OpenAIClient: IOpenAIClient
{
    private HttpClient _httpClient;

    public ChatEndpoint ChatCompletion
    {
        get => new ChatEndpoint(_httpClient);
    }

    public ImageEndpoint Image
    {
        get => new ImageEndpoint(_httpClient);
    }

    public AudioEndpoint Audio
    {
        get => new AudioEndpoint(_httpClient);
    }

    public ModerationEndpoint Moderation
    {
        get => new ModerationEndpoint(_httpClient);
    }

    public OpenAIClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
}