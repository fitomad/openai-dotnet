using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fitomad.OpenAI.Endpoints;
using System.ComponentModel;
using Fitomad.OpenAI;
using Fitomad.OpenAI.Entities.Models;
using Fitomad.OpenAI.Endpoints.Models;

namespace Fitomad.OpenAI.Tests;

public class ModelTests
{
    private string _apiKey;
    private IOpenAIClient _client;

    public ModelTests()
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
    public async Task Models_TestList()
    {
        Assert.NotNull(_client);

        ModelListResponse response = await _client.Models.List();
        
        Assert.NotNull(response);
        Assert.NotEmpty(response.Results);
        
        foreach(var model in response.Results)
        {
            Assert.NotEmpty(model.ModelId);
            Assert.NotEmpty(model.Owner);
            Assert.NotEmpty(model.ObjectType);
            Assert.NotEqual(0, model.CreatedDate);
            Console.WriteLine(model.ModelId);
        }
    }

    [Theory]
    [InlineData("gpt-4")]
    [InlineData("gpt-3.5-turbo-0301")]
    [InlineData("dall-e-3")]
    [InlineData("ada-similarity")]
    [InlineData("code-search-babbage-text-001")]
    public async Task Models_TestRetrieve(string modelName)
    {
        Assert.NotNull(_client);

        ModelResponse response = await _client.Models.Retrieve(model: modelName);
    }
}