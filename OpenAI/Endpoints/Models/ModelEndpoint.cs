using System.Text;
using System.Text.Json;
using System.Net.Http.Json;
using Fitomad.OpenAI.Entities.Models;

namespace Fitomad.OpenAI.Endpoints.Models;

public interface IModelEndpoint
{
    public Task<ModelListResponse> List();
    public Task<ModelResponse> Retrieve(string model);
    public Task<ModelDeletedResponse> Delete(string model);
}

public class ModelEndpoint: IModelEndpoint
{
    private HttpClient _httpClient;

    internal ModelEndpoint(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ModelListResponse> List()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(Endpoint.List);
        var modelListResponse = await response.Content.ReadFromJsonAsync<ModelListResponse>();

        return modelListResponse;
    }

    public async Task<ModelResponse> Retrieve(string model)
    {
        var retrieveUri = $"{Endpoint.Retrieve}/{model}";
        HttpResponseMessage response = await _httpClient.GetAsync(retrieveUri);
        var modelRetrieveResponse = await response.Content.ReadFromJsonAsync<ModelResponse>();

        return modelRetrieveResponse;
    }

    public async Task<ModelDeletedResponse> Delete(string model)
    {
        var deleteUri = $"{Endpoint.Delete}/{model}";
        HttpResponseMessage response = await _httpClient.DeleteAsync(deleteUri);
        var modelDeleteResponse = await response.Content.ReadFromJsonAsync<ModelDeletedResponse>();

        return modelDeleteResponse;
    }

    internal static class Endpoint
    {
        internal const string List = "v1/models";
        internal const string Retrieve = "v1/models";
        internal const string Delete = "v1/models/";
    }
}