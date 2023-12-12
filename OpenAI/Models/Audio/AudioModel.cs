using System.Text;
using System.Text.Json;
using System.Net.Http.Json;
using Fitomad.OpenAI.Entities.Audio;
using System.Net.Http.Headers;

namespace Fitomad.OpenAI.Models.Audio;

public sealed class AudioModel
{
    private HttpClient _httpClient;

    internal AudioModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<SpeechResponse> CreateSpeech(SpeechRequest request)
    {
        var payload = JsonSerializer.Serialize(request);
        var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync(Endpoint.Speech, httpContent);

        var speechResponse = await response.Content.ReadFromJsonAsync<SpeechResponse>();

        return speechResponse;
    }

    public async Task<TranscriptionResponse> CreateTranscription(TranscriptionRequest request)
    {
        var payload = JsonSerializer.Serialize(request);
        var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync(Endpoint.Transcription, httpContent);

        var transcriptionResponse = await response.Content.ReadFromJsonAsync<TranscriptionResponse>();

        return transcriptionResponse;
    }

    public async Task<TranslationResponse> CreateTranslation(TranslationRequest request)
    {
        var payload = JsonSerializer.Serialize(request);
        var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync(Endpoint.Transcription, httpContent);

        var translationResponse = await response.Content.ReadFromJsonAsync<TranslationResponse>();

        return translationResponse;
    }

    internal static class Endpoint
    {
        internal const string Speech = "v1/audio/speech";
        internal const string Transcription = "v1/audio/transcriptions";
        internal const string Translation = "v1/audio/translations";
    }
}