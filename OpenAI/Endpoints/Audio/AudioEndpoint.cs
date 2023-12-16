using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Json;
using Fitomad.OpenAI.Entities.Audio;

namespace Fitomad.OpenAI.Endpoints.Audio;

public interface IAudioEndpoint
{
    public Task<SpeechResponse> CreateSpeech(SpeechRequest request);
    public Task<TranscriptionResponse> CreateTranscription(TranscriptionRequest request);
    public Task<TranslationResponse> CreateTranslation(TranslationRequest request);
}

public sealed class AudioEndpoint: IAudioEndpoint
{
    private HttpClient _httpClient;

    internal AudioEndpoint(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<SpeechResponse> CreateSpeech(SpeechRequest request)
    {
        var payload = JsonSerializer.Serialize(request);
        var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync(Endpoint.Speech, httpContent);
        byte[] soundContentFile = await response.Content.ReadAsByteArrayAsync();
        
        var speechResponse = new SpeechResponse
        {
            ResponseFormat = request.ResponseFormat,
            Content = soundContentFile
        };

        return speechResponse;
    }

    public async Task<TranscriptionResponse> CreateTranscription(TranscriptionRequest request)
    {
        var multipartContent = MakeMultipartContent(request: request);
        HttpResponseMessage response = await _httpClient.PostAsync(Endpoint.Transcription, multipartContent);

        TranscriptionResponse transcriptionResponse;

        switch(request.ResponseFormat)
        {
            case "json":
            case "verbose_json":
                transcriptionResponse = await response.Content.ReadFromJsonAsync<TranscriptionResponse>();
                break;
            default:
                transcriptionResponse = new TranscriptionResponse
                {
                    Text = await response.Content.ReadAsStringAsync()
                };
                break;
        }

        return transcriptionResponse;
    }

    public async Task<TranslationResponse> CreateTranslation(TranslationRequest request)
    {
        var multipartContent = MakeMultipartContent(request: request);
        HttpResponseMessage response = await _httpClient.PostAsync(Endpoint.Translation, multipartContent);

        TranslationResponse translationResponse;

        switch(request.ResponseFormat)
        {
            case "json":
            case "verbose_json":
                translationResponse = await response.Content.ReadFromJsonAsync<TranslationResponse>();
                break;
            default:
                translationResponse = new TranslationResponse
                {
                    Text = await response.Content.ReadAsStringAsync()
                };
                break;
        }

        return translationResponse;
    }

    private MultipartFormDataContent MakeMultipartContent(TranscriptionRequest request)
    {
        var multipartContent = new MultipartFormDataContent();

        multipartContent.Add(new StringContent(request.Model), "model");
        multipartContent.Add(new StringContent(request.ResponseFormat), "response_format");
        multipartContent.Add(new StringContent(request.TemperatureStringValue), "temperature");

        var fileStream = File.OpenRead(request.File);
        var memoryStream =new MemoryStream();
        fileStream.CopyTo(memoryStream);

        multipartContent.Add(new ByteArrayContent(memoryStream.ToArray()), "file", fileName: request.FileName);
        
        if(TranscriptionRequest.IsValid(request.Language))
        {
            multipartContent.Add(new StringContent(request.Language), "language");
        }

        if(TranscriptionRequest.IsValid(request.Prompt))
        {
            multipartContent.Add(new StringContent(request.Prompt), "prompt");
        }

        memoryStream.Close();

        return multipartContent;
    }

    private MultipartFormDataContent MakeMultipartContent(TranslationRequest request)
    {
        var multipartContent = new MultipartFormDataContent();

        multipartContent.Add(new StringContent(request.Model), "model");
        multipartContent.Add(new StringContent(request.ResponseFormat), "response_format");
        multipartContent.Add(new StringContent(request.TemperatureStringValue), "temperature");

        var fileStream = File.OpenRead(request.File);
        var memoryStream =new MemoryStream();
        fileStream.CopyTo(memoryStream);

        multipartContent.Add(new ByteArrayContent(memoryStream.ToArray()), "file", fileName: request.FileName);
        
        if(TranscriptionRequest.IsValid(request.Language))
        {
            multipartContent.Add(new StringContent(request.Language), "language");
        }

        if(TranscriptionRequest.IsValid(request.Prompt))
        {
            multipartContent.Add(new StringContent(request.Prompt), "prompt");
        }

        memoryStream.Close();

        return multipartContent;
    }

    internal static class Endpoint
    {
        internal const string Speech = "v1/audio/speech";
        internal const string Transcription = "v1/audio/transcriptions";
        internal const string Translation = "v1/audio/translations";
    }
}