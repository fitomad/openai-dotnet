using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using OpenAI;

namespace OpenAI.Extensions;

public static class ServiceColletionOpenAI
{
    //private static string HttpClientName = "TechTalkGame.HttpClient.OpenAI";
    private static string OpenAIApiKeyHeader = "Bearer";
    private static string OpenAIOrganizationHeader = "OpenAI-Organization";
    
    public static void AddOpenAIHttpClient(this IServiceCollection services, OpenAISettings settings) 
    {
        services.AddHttpClient<IOpenAIClient, OpenAIClient>(client =>
        //services.AddHttpClient(HttpClientName, client => 
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