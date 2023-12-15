using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fitomad.OpenAI.Endpoints;
using System.ComponentModel;
using Fitomad.OpenAI;
using Fitomad.OpenAI.Entities.Audio;
using Fitomad.OpenAI.Endpoints.Audio;

namespace Fitomad.OpenAI.Tests;

public class AudioTests
{
    private const string ElQuijote = "En un lugar de la Mancha, de cuyo nombre no quiero acordarme, no ha mucho tiempo que vivía un hidalgo de los de lanza en astillero, adarga antigua, rocín flaco y galgo corredor. Una olla de algo más vaca que carnero, salpicón las más noches, duelos y quebrantos los sábados, lantejas los viernes, algún palomino de añadidura los domingos, consumían las tres partes de su hacienda.";
    
    private string _apiKey;
    private IOpenAIClient _client;


    public AudioTests()
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<ChatCompletionTests>()
            .Build();

        _apiKey = configuration.GetValue<string>("OpenAI:ApiKey");

         var testSettings = new OpenAISettingsBuilder()
            .WithApiKey(_apiKey!)
            .Build();

        var services = new ServiceCollection();
        services.AddOpenAIHttpClient(settings: testSettings);
        var provider = services.BuildServiceProvider();
        
        _client = provider.GetRequiredService<IOpenAIClient>();
    }

    [Fact]
    public async Task Audio_TestSpeech()
    {
        Assert.NotNull(_client);

        SpeechRequest request = new SpeechRequestBuilder()
            .WithModel(SpeechModelType.TTS_1)
            .WithVoice(VoiceType.Onyx)
            .WithResponseFormat(SpeechResponseFormat.MP3)
            .WithInput(ElQuijote)
            .Build();

        SpeechResponse response = await _client.Audio.CreateSpeech(request);

        Assert.NotNull(response);
        Assert.Equal("mp3", response.ResponseFormat);
        Assert.NotEmpty(response.Content);

        response.SaveToFile("/Users/adolfo/Desktop/testing-audio.mp3");
    }

    [Fact]
    public async Task Audio_TestTranscription()
    {
        Assert.NotNull(_client);

        TranscriptionRequest request = new TranscriptionRequestBuilder()
            .WithModel(TranscriptionModelType.Whisper1)
            .WithResponseFormat(TranscriptionResponseFormat.Text)
            .WithFile("")
            .Build();

        TranscriptionResponse response = await _client.Audio.CreateTranscription(request);

        Assert.NotNull(response);
        Assert.NotEmpty(response.Text);
    }
}