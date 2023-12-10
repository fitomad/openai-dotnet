using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fitomad.OpenAI.Models.Chat;
using Fitomad.OpenAI.Entities.Chat;
using System.ComponentModel;
using Fitomad.OpenAI.Extensions;

namespace Fitomad.OpenAI.Tests;

public class ChatCompletionTests
{
    private string _apiKey;

    public ChatCompletionTests()
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<ChatCompletionTests>()
            .Build();

        _apiKey = configuration.GetValue<string>("OpenAI:ApiKey");
    }

    [Theory]
    [InlineData(ChatModelKind.GPT_3_5_TURBO)]
    [InlineData(ChatModelKind.GPT_3_5_TURBO_16K)]
    [InlineData(ChatModelKind.GPT_4_32K)]
    [InlineData(ChatModelKind.GPT_4)]
    [InlineData(ChatModelKind.GPT_4_VISION_PREVIEW)]
    [InlineData(ChatModelKind.GPT_4_1106_PREVIEW)]
    public void Chat_Models(ChatModelKind kind)
    {
        ChatRequest request = new ChatRequestBuilder()
            .WithModel(kind)
            .WithUserMessage("¿Cuál es la distancia de la Tierra al Sol?")
            .WithTemperatute(TemperatureKind.Precise)
            .Build();
        
    }

    [Fact]
    public void Chat_NoModel()
    {
        Assert.Throws<OpenAIException>(() =>
        {
            ChatRequest request = new ChatRequestBuilder()
                .WithUserMessage("¿Cuál es la distancia de la Tierra al Sol?")
                .WithTemperatute(0.8)
                .Build();
        });
    }

    [Fact]
    public void Chat_NoMessage()
    {
        Assert.Throws<OpenAIException>(() =>
        {
            ChatRequest request = new ChatRequestBuilder()
                .WithModel(ChatModelKind.GPT_3_5_TURBO)
                .WithTemperatute(0.8)
                .Build();
        });
        
    }

    [Fact]
    public void Chat_NoModelNoMessage()
    {
        Assert.Throws<OpenAIException>(() =>
        {
            ChatRequest request = new ChatRequestBuilder()
                .WithTemperatute(0.8)
                .Build();
        });
    }

    [Fact]
    public void Chat_SystemAndUserMessages()
    {
        ChatRequest request = new ChatRequestBuilder()
            .WithModel(ChatModelKind.GPT_3_5_TURBO)
            .WithSystemMessage("Eres un profesor universitario de Astrofísica.")
            .WithUserMessage("¿En qué consiste la Constante Cosmológica de Einstein?")
            .WithTemperatute(TemperatureKind.Precise)
            .Build();
    }

    [Fact]
    public void Chat_TemperatureOutOfRange_Bottom()
    {
        Assert.Throws<OpenAIException>(() =>
        {
            ChatRequest request = new ChatRequestBuilder()
                .WithModel(ChatModelKind.GPT_3_5_TURBO)
                .WithSystemMessage("Eres un profesor universitario de Astrofísica.")
                .WithUserMessage("¿En qué consiste la Constante Cosmológica de Einstein?")
                .WithTemperatute(-3.0)
                .Build();
        });
    }

    [Fact]
    public void Chat_TemperatureOutOfRange_Top()
    {
        Assert.Throws<OpenAIException>(() =>
        {
            ChatRequest request = new ChatRequestBuilder()
                .WithModel(ChatModelKind.GPT_3_5_TURBO)
                .WithSystemMessage("Eres un profesor universitario de Astrofísica.")
                .WithUserMessage("¿En qué consiste la Constante Cosmológica de Einstein?")
                .WithTemperatute(3.0)
                .Build();
        });
    }

    [Fact]
    public void Chat_FrequencyOutOfRange_Bottom()
    {
        Assert.Throws<OpenAIException>(() =>
        {
            ChatRequest request = new ChatRequestBuilder()
                .WithModel(ChatModelKind.GPT_3_5_TURBO)
                .WithSystemMessage("Eres un profesor universitario de Astrofísica.")
                .WithUserMessage("¿En qué consiste la Constante Cosmológica de Einstein?")
                .WithFrequencyPenalty(-3.0)
                .Build();
        });
    }

    [Fact]
    public void Chat_FrequencyOutOfRange_Top()
    {
        Assert.Throws<OpenAIException>(() =>
        {
            ChatRequest request = new ChatRequestBuilder()
                .WithModel(ChatModelKind.GPT_3_5_TURBO)
                .WithSystemMessage("Eres un profesor universitario de Astrofísica.")
                .WithUserMessage("¿En qué consiste la Constante Cosmológica de Einstein?")
                .WithFrequencyPenalty(3.0)
                .Build();
        });
    }

    [Fact]
    public void Chat_PresenceOutOfRange_Bottom()
    {
        Assert.Throws<OpenAIException>(() =>
        {
            ChatRequest request = new ChatRequestBuilder()
                .WithModel(ChatModelKind.GPT_3_5_TURBO)
                .WithSystemMessage("Eres un profesor universitario de Astrofísica.")
                .WithUserMessage("¿En qué consiste la Constante Cosmológica de Einstein?")
                .WithPresencePenalty(-3.0)
                .Build();
        });
    }

    [Fact]
    public void Chat_PresenceOutOfRange_Top()
    {
        Assert.Throws<OpenAIException>(() =>
        {
            ChatRequest request = new ChatRequestBuilder()
                .WithModel(ChatModelKind.GPT_3_5_TURBO)
                .WithSystemMessage("Eres un profesor universitario de Astrofísica.")
                .WithUserMessage("¿En qué consiste la Constante Cosmológica de Einstein?")
                .WithPresencePenalty(3.0)
                .Build();
        });
    }

    [Fact]
    public async Task ChatRequest_Test()
    {
        ChatRequest request = new ChatRequestBuilder()
            .WithModel(ChatModelKind.GPT_3_5_TURBO)
            .WithSystemMessage("Eres un profesor de alumnos de 10 años.")
            .WithUserMessage("Explícame qué es una estrella.")
            .WithTemperatute(TemperatureKind.Precise)
            .WithReponseFormat(ChatResponseFormat.Text)
            .Build();

        var testSettings = new OpenAISettingsBuilder()
            .WithApiKey(_apiKey)
            .Build();

        var services = new ServiceCollection();
        services.AddOpenAIHttpClient(settings: testSettings);
        var provider = services.BuildServiceProvider();
        
        var client = provider.GetRequiredService<IOpenAIClient>();
        Assert.NotNull(client);

        var chatResponse = await client.ChatCompletion.CreateChatAsync(request);
        Assert.NotNull(chatResponse);
        Assert.NotEmpty(chatResponse.ResponseId);
        Assert.NotEmpty(chatResponse.Choices);
    }
}