using Microsoft.VisualBasic;

namespace OpenAI;

public struct OpenAISettings
{
    private string _apiKey;
    private string? _organizationId;

    public string ApiKey 
    {
        get => _apiKey;
        internal set => _apiKey = value;
    }

    public string? OrganzationId
    {
        get => _organizationId;
        internal set => _organizationId = value;
    }

    public bool IsOrganizationAvailable
    {
        get => _organizationId is not null;
    }
}
