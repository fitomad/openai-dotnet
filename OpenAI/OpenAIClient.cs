using Fitomad.OpenAI.Models.Audio;
using Fitomad.OpenAI.Models.Chat;
using Fitomad.OpenAI.Models.Image;

namespace Fitomad.OpenAI;

public interface IOpenAIClient
{
    public ChatModel ChatCompletion { get; }
    public ImageModel Image { get; }
    public AudioModel Audio { get; }
}

public class OpenAIClient: IOpenAIClient
{
    private HttpClient _httpClient;

    public ChatModel ChatCompletion
    {
        get => new ChatModel(_httpClient);
    }

    public ImageModel Image
    {
        get => new ImageModel(_httpClient);
    }

    public AudioModel Audio
    {
        get => new AudioModel(_httpClient);
    }

    public OpenAIClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
}