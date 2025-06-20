using System.Text.Json;
using JakDojadeMCP.Server.DTO;
using JakDojadeMCP.Server.Helpers;
using JakDojadeMCP.Server.Models;

namespace JakDojadeMCP.Server.Clients;

public class JakDojadeClient(HttpClient httpClient)
{
    public async Task<GetCitiesResponseDto?> GetCitiesAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/rest/v1/cities");
        
        return await HandleRequest<GetCitiesResponseDto>(request);
    }

    public async Task<GetLocationsResponseDto?> GetLocationsAsync(string agglomeration, string searchPhrase)
    {
        var endpoint = $"/api/rest/v1/locationmatcher?agg={agglomeration}&text={searchPhrase}";
        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
        
        return await HandleRequest<GetLocationsResponseDto>(request);
    }

    public async Task<ScheduleTable?> GetScheduleTableAsync(int @operator, string? lineSymbol, string stopCode)
    {
        var lineParam = lineSymbol != null ? $"&l={lineSymbol}" : string.Empty;
        var endpoint = $"/api/rest/v1/schedule/table?op={@operator}&s={stopCode}{lineParam}";
        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
        
        return await HandleRequest<ScheduleTable>(request);
    }

    public async Task<GetRoutesResponseDto?> GetRoutesAsync(FindRoute findRoute)
    {
        var query = QueryStringBuilder.ToQueryString(findRoute);
        var endpoint = $"/api/rest/v2/routes?{query}";
        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
        
        return await HandleRequest<GetRoutesResponseDto>(request);
    }

    private async Task<T?> HandleRequest<T>(HttpRequestMessage request)
    {
        var response = await httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            Console.Error.WriteLine(body);
        }
        response.EnsureSuccessStatusCode();
        
        var responseBody = await response.Content.ReadAsStreamAsync();
        
        return await JsonSerializer.DeserializeAsync<T>(responseBody, JsonSerializerOptions.Web);
    }
}