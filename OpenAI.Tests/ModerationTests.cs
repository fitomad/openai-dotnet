using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fitomad.OpenAI.Endpoints;
using System.ComponentModel;
using Fitomad.OpenAI;
using Fitomad.OpenAI.Entities.Moderation;
using Fitomad.OpenAI.Endpoints.Moderation;

namespace Fitomad.OpenAI.Tests;

public class ModerationTests
{
    private const string ElQuijote = "En un lugar de la Mancha, de cuyo nombre no quiero acordarme, no ha mucho tiempo que vivía un hidalgo de los de lanza en astillero";
    private const string CaboTrafalgar = "El teniente de navío Louis Quelennec, de la Marina Imperial francesa, está a punto de figurar en los libros de Historia y en este relato, pero no lo sabe. De lo contrario, sus primeras palabras al amanecer el 29 de vendimiario del año XIV, o sea, el 21 de octubre de 1805, habrían sido otras";
    
    private const string HateWords = "Odio tener que ir a ese sitio que tanto asco me da para ver a esa gente que no soporto.";
    
    private string _apiKey;
    private IOpenAIClient _client;


    public ModerationTests()
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
    public void Test_FailureNoParametersBuilding()
    {
        Assert.Throws<OpenAIException>(() =>
        {
            var request = new ModerationRequestBuilder()
                .Build();
        });
    }

    [Fact]
    public void Test_FailureNoModelBuilding()
    {
        Assert.Throws<OpenAIException>(() =>
        {
            var request = new ModerationRequestBuilder()
                .WithModel(string.Empty)
                .Build();
        });
    }

    [Fact]
    public void Test_FailureNoInputBuilding()
    {
        Assert.Throws<OpenAIException>(() =>
        {
            var request = new ModerationRequestBuilder()
                .WitnInput(string.Empty)
                .WithModel(ModerationModelType.TextModerationStable)
                .Build();
        });
    }

    [Theory]
    [InlineData(ElQuijote)]
    [InlineData(CaboTrafalgar)]
    public async Task ModerationTest_ValidInputsGoodVibes(string userInput)
    {
        var moderationRequest = new ModerationRequestBuilder()
            .WitnInput(userInput)
            .WithModel(ModerationModelType.TextModerationLatest)
            .Build();

        ModerationResponse response = await _client.Moderation.CreateModeration(moderationRequest);

        Assert.NotEmpty(response.Results);
        Assert.NotNull(response.Results[0]);
        Assert.False(response.Results[0].Values.Sexual);
        Assert.False(response.Results[0].Values.Hate);
        Assert.False(response.Results[0].Values.ViolenceGraphic);
    }

    [Theory]
    [InlineData(HateWords)]
    public async Task ModerationTest_ValidInputsBadVibes(string userInput)
    {
        var moderationRequest = new ModerationRequestBuilder()
            .WitnInput(userInput)
            .WithModel(ModerationModelType.TextModerationLatest)
            .Build();

        ModerationResponse response = await _client.Moderation.CreateModeration(moderationRequest);

        Assert.NotEmpty(response.Results);
        Assert.NotNull(response.Results[0]);
        Assert.True(response.Results[0].Values.Hate);
    }
}