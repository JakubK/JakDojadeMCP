using System.Text.Json;
using JakDojadeMCP.Server.DTO;
using JakDojadeMCP.Server.Helpers;
using JakDojadeMCP.Server.Models;

namespace JakDojadeMCP.Server.Clients;

public class JakDojadeClient(HttpClient httpClient)
{
    public async Task<IEnumerable<City>> GetCitiesAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/rest/v1/cities");
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStreamAsync();
        
        var deserialized = await JsonSerializer.DeserializeAsync<GetCitiesResponseDto>(responseBody);
        return deserialized?.Cities ?? [];
    }

    public async Task<IEnumerable<Location>> GetLocationsAsync(string agglomeration, string searchPhrase)
    {
        var endpoint = $"/api/rest/v1/locationmatcher?agg={agglomeration}&text={searchPhrase}";
        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStreamAsync();
        var deserialized = await JsonSerializer.DeserializeAsync<GetLocationsResponseDto>(responseBody);

        return deserialized?.Locations ?? [];
    }

    public async Task<ScheduleTable?> GetScheduleTableAsync(int @operator, string? lineSymbol, string stopCode)
    {
        var lineParam = lineSymbol != null ? $"&l={lineSymbol}" : string.Empty;
        var endpoint = $"/api/rest/v1/schedule/table?op={@operator}&s={stopCode}{lineParam}";
        
        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<ScheduleTable>(responseBody);
    }

    public async Task<IEnumerable<Route>> GetRoutesAsync(FindRoute findRoute)
    {
        var query = QueryStringBuilder.ToQueryString(findRoute);
        var endpoint = $"/api/rest/v2/routes?{query}";
        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
        var response = await httpClient.SendAsync(request);
        var responseBody = await response.Content.ReadAsStreamAsync();
        var deserialized = await JsonSerializer.DeserializeAsync<GetRoutesResponseDto>(responseBody);

        return deserialized?.Routes ?? [];
    }
}