using System.Net.Http.Headers;
using System.Reflection.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Fitomad.OpenAI.Extensions;

public static class ServiceColletionOpenAI
{
    private static string OpenAIApiKeyHeader = "Bearer";
    private static string OpenAIOrganizationHeader = "OpenAI-Organization";
    
    public static void AddOpenAIHttpClient(this IServiceCollection services, OpenAISettings settings) 
    {
        services.AddHttpClient<IOpenAIClient, OpenAIClient>(client =>
        {
            var openAIBaseUri = "https://api.openai.com";
            client.BaseAddress = new Uri(openAIBaseUri);

            var jsonMediaType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(jsonMediaType);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(OpenAIApiKeyHeader, settings.ApiKey);

            if(settings.IsOrganizationAvailable)
            {
                client.DefaultRequestHeaders.Add(OpenAIOrganizationHeader, settings.OrganzationId);
            }
        });
    }
}