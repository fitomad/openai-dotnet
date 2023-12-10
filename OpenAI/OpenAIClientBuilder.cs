using System.Runtime.CompilerServices;

namespace OpenAI;

public class OpenAISettingsBuilder
{
    private const string UserSecretApiKey = "";
    private OpenAISettings _settings;

    public OpenAISettingsBuilder()
    {
        _settings = new OpenAISettings();
    }

    public OpenAISettingsBuilder WithApiKey(string apiKey)
    {
        _settings.ApiKey = apiKey;
        return this;
    }

    public OpenAISettingsBuilder WithApiKeyFromEnvironmentVariable(string name)
    {
        var key = Environment.GetEnvironmentVariable(name);

        if(key is not null)
        {
            _settings.ApiKey = key;
        }

        return this;
    }

    public OpenAISettingsBuilder WithOrganizationIdentifier(string organizationId)
    {
        _settings.OrganzationId = organizationId;
        return this;
    }

    public OpenAISettingsBuilder WithOrganizationIdentifierFromEnvironmentVariable(string name)
    {
        _settings.OrganzationId = Environment.GetEnvironmentVariable(name);
        return this;
    }

    public OpenAISettings Build()
    {
        if(string.IsNullOrEmpty(_settings.ApiKey))
        {
            throw new OpenAIException("Yoy must specified an API key");
        }

        return _settings;
    }
}