using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fitomad.OpenAI.Endpoints.Image;
using Fitomad.OpenAI.Entities.Image;
using System.ComponentModel;
using Fitomad.OpenAI.Extensions;
using System.Text;

namespace Fitomad.OpenAI.Tests;

public class ImageTests
{
    private string _apiKey;

    public ImageTests()
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<ImageTests >()
            .Build();

        _apiKey = configuration.GetValue<string>("OpenAI:ApiKey");
    }

    [Fact]
    public void ImageTest_NoPrompt()
    {
        Assert.Throws<OpenAIException>(() =>
        {
            var request = new ImageRequestBuilder()
                .Build();
        });
    }

    [Fact]
    public void ImageTest_PropmtExceedLengthDallE2Test()
    {
        var stringBuilder = new StringBuilder();

        for(int index = 0; index < 1_001; index++)
        {
            stringBuilder.Append("A");
        }

        Assert.Throws<OpenAIException>(() =>
        {
            var request = new ImageRequestBuilder()
                .WithModel(ImageModelType.DALL_E_2)
                .WithPrompt(stringBuilder.ToString())
                .Build();
        });
    }

    [Fact]
    public void ImageTest_PropmtExceedLengthDallE3Test()
    {
        var stringBuilder = new StringBuilder();

        for(int index = 0; index < 4_001; index++)
        {
            stringBuilder.Append("A");
        }

        Assert.Throws<OpenAIException>(() =>
        {
            var request = new ImageRequestBuilder()
                .WithModel(ImageModelType.DALL_E_3)
                .WithPrompt(stringBuilder.ToString())
                .Build();
        });
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(0)]
    [InlineData(11)]
    public void ImageTest_ImageCountFailureDallE2(int imagesCount)
    {
        Assert.Throws<OpenAIException>(() =>
        {
            var request = new ImageRequestBuilder()
                .WithModel(ImageModelType.DALL_E_2)
                .WithPrompt("Testing image count")
                .WithImagesCount(imagesCount)
                .Build();
        });
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(0)]
    [InlineData(2)]
    public void ImageTest_ImageCountFailureDallE3(int imagesCount)
    {
        Assert.Throws<OpenAIException>(() =>
        {
            var request = new ImageRequestBuilder()
                .WithModel(ImageModelType.DALL_E_3)
                .WithPrompt("Testing image count")
                .WithImagesCount(imagesCount)
                .Build();
        });
    }

    [Fact]
    public async Task ImageRequest_Test()
    {
        ImageRequest request = new ImageRequestBuilder()
            .WithModel(ImageModelType.DALL_E_3)
            .WithPrompt("Un paisaje urbano, con algunos rascacielos de fondo aplicando un estilo de Dalí.")
            .WithImagesCount(1)
            .WithSize(DallE3Size.Square)
            .WithQuality(DallE3Quality.HD)
            .WithStyle(DallE3Style.Vivid)
            .WithResponseFormat(ImageResponseFormat.Url)
            .Build();

        var testSettings = new OpenAISettingsBuilder()
            .WithApiKey(_apiKey)
            .Build();

        var services = new ServiceCollection();
        services.AddOpenAIHttpClient(settings: testSettings);
        var provider = services.BuildServiceProvider();
        
        var client = provider.GetRequiredService<IOpenAIClient>();
        Assert.NotNull(client);

        ImageResponse imageResponse = await client.Image.CreateImageAsync(request);
        
        Assert.NotNull(imageResponse);
        Assert.NotNull(imageResponse.Images);
        Assert.NotEmpty(imageResponse.Images);
    }
}