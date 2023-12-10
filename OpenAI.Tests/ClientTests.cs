namespace OpenAI.Tests;

using OpenAI.Models.Chat;

public class ClientTests
{
    [Theory]
    [InlineData("API_TEST", "ORGANIZATION_TEST")]
    public void OpenAIClient_WithApiAndOrganization(string apiKey, string organizationId)
    {
        var settings = new OpenAISettingsBuilder()
            .WithApiKey(apiKey)
            .WithOrganizationIdentifier(organizationId)
            .Build();

        Assert.Equal(settings.ApiKey, apiKey);
        Assert.Equal(settings.OrganzationId, organizationId);
    }

    [Theory]
    [InlineData("API_TEST")]
    public void OpenAIClient_WithApi(string apiKey)
    {
        var settings = new OpenAISettingsBuilder()
            .WithApiKey(apiKey)
            .Build();

        Assert.Equal(settings.ApiKey, apiKey);
        Assert.Null(settings.OrganzationId);
    }

    [Theory]
    [InlineData("ORGANIZATION_TEST")]
    public void OpenAIClient_WithoutApiKeyWithOrganization(string organizationId)
    { 
        Assert.Throws<OpenAIException>(() =>
        {
            var settings = new OpenAISettingsBuilder()
                .WithOrganizationIdentifier(organizationId)
                .Build();   
        });
    }

    [Fact]
    public void OpenAIClient_NoSettings()
    {
        Assert.Throws<OpenAIException>(() =>
        {
            var client = new OpenAISettingsBuilder()
                .Build();
        });
    }
}