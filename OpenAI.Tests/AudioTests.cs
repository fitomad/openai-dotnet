using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fitomad.OpenAI.Models;
using System.ComponentModel;
using Fitomad.OpenAI;
using Fitomad.OpenAI.Entities.Audio;
using Fitomad.OpenAI.Models.Audio;

namespace Fitomad.OpenAI.Tests;

public class AudioTests
{
    private const string ElQuijote = "En un lugar de la Mancha, de cuyo nombre no quiero acordarme, no ha mucho tiempo que vivía un hidalgo de los de lanza en astillero";
    
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
            .WithVoice(VoiceType.Nova)
            .WithResponseFormat(SpeechResponseFormat.MP3)
            .WithInput(ElQuijote)
            .Build();

        SpeechResponse response = await _client.Audio.CreateSpeech(request);
    }
}