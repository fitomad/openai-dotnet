using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fitomad.OpenAI.Endpoints;
using Fitomad.OpenAI.Endpoints.Chat;
using Fitomad.OpenAI.Entities.Chat;
using System.ComponentModel;
using Fitomad.OpenAI.Extensions;

namespace Fitomad.OpenAI.Tests;

public class ChatCompletionTests
{
    private string _apiKey;
    private IOpenAIClient _client;

    public ChatCompletionTests()
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

    [Theory]
    [InlineData(ChatModelType.GPT_3_5_TURBO)]
    [InlineData(ChatModelType.GPT_3_5_TURBO_16K)]
    [InlineData(ChatModelType.GPT_4_32K)]
    [InlineData(ChatModelType.GPT_4)]
    [InlineData(ChatModelType.GPT_4_VISION_PREVIEW)]
    [InlineData(ChatModelType.GPT_4_1106_PREVIEW)]
    public void Chat_Models(ChatModelType kind)
    {
        ChatRequest request = new ChatRequestBuilder()
            .WithModel(kind)
            .WithUserMessage("¿Cuál es la distancia de la Tierra al Sol?")
            .WithTemperatute(Temperature.Precise)
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
                .WithModel(ChatModelType.GPT_3_5_TURBO)
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
            .WithModel(ChatModelType.GPT_3_5_TURBO)
            .WithSystemMessage("Eres un profesor universitario de Astrofísica.")
            .WithUserMessage("¿En qué consiste la Constante Cosmológica de Einstein?")
            .WithTemperatute(Temperature.Precise)
            .Build();
    }

    [Fact]
    public void Chat_TemperatureOutOfRange_Bottom()
    {
        Assert.Throws<OpenAIException>(() =>
        {
            ChatRequest request = new ChatRequestBuilder()
                .WithModel(ChatModelType.GPT_3_5_TURBO)
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
                .WithModel(ChatModelType.GPT_3_5_TURBO)
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
                .WithModel(ChatModelType.GPT_3_5_TURBO)
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
                .WithModel(ChatModelType.GPT_3_5_TURBO)
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
                .WithModel(ChatModelType.GPT_3_5_TURBO)
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
                .WithModel(ChatModelType.GPT_3_5_TURBO)
                .WithSystemMessage("Eres un profesor universitario de Astrofísica.")
                .WithUserMessage("¿En qué consiste la Constante Cosmológica de Einstein?")
                .WithPresencePenalty(3.0)
                .Build();
        });
    }

    [Fact]
    public async Task ChatRequest_Test()
    {
        Assert.NotNull(_client);

        ChatRequest request = new ChatRequestBuilder()
            .WithModel(ChatModelType.GPT_3_5_TURBO)
            .WithSystemMessage("Eres un profesor de alumnos de 10 años.")
            .WithUserMessage("Explícame qué es una estrella.")
            .WithTemperatute(Temperature.Precise)
            .WithReponseFormat(ChatResponseFormat.Text)
            .Build();

        var chatResponse = await _client.ChatCompletion.CreateChatAsync(request);
        Assert.NotNull(chatResponse);
        Assert.NotEmpty(chatResponse.ResponseId);
        Assert.NotEmpty(chatResponse.Choices);
    }

    [Theory]
    [InlineData("https://upload.wikimedia.org/wikipedia/commons/a/ae/Vel%C3%A1zquez_-_La_Fragua_de_Vulcano_%28Museo_del_Prado%2C_1630%29.jpg", "¿Qué cuadro es este?")]
    [InlineData("https://farm6.staticflickr.com/5576/14958790671_70e7be5568_o_d.jpg", "¿Qué se ve en la imagen?")]
    [InlineData("https://www.que-leer.com/wp-content/uploads/2023/09/STEPHEM-KING-foto-portada-e-interior.jpg", "¿Qué se ve en esta imagen")]
    [InlineData("https://upload.wikimedia.org/wikipedia/commons/5/57/M31bobo.jpg", "¿Qué galaxia es la que se ve en la imagen?")]
    public async Task Chat_ImageExplantion(string imageUrl, string question)
    {
        Assert.NotNull(_client);

        var imageExplanationResponse = await _client.ChatCompletion.ExplainImageAsync(imageUrl, userQuestion: question);
        Assert.NotNull(imageExplanationResponse);
        Assert.NotEmpty(imageExplanationResponse.ResponseId);
        Assert.NotEmpty(imageExplanationResponse.Choices);
    }
}