using System.Net.Http.Json;
using System.Text.Json;
using JakDojadeMCP.Server.DTO;
using JakDojadeMCP.Server.Helpers;
using JakDojadeMCP.Server.Models;

namespace JakDojadeMCP.Server.Clients;

public class JakDojadeClient(HttpClient httpClient)
{
    public async Task<GetCitiesResponseDto?> GetCitiesAsync()
    {
        return await httpClient.GetFromJsonAsync<GetCitiesResponseDto>("/api/rest/v1/cities", JsonSerializerOptions.Web);
    }

    public async Task<GetLocationsResponseDto?> GetLocationsAsync(string agglomeration, string searchPhrase)
    {
        var endpoint = $"/api/rest/v1/locationmatcher?agg={agglomeration}&text={searchPhrase}";
        return await httpClient.GetFromJsonAsync<GetLocationsResponseDto>(endpoint, JsonSerializerOptions.Web);
    }

    public async Task<ScheduleTable?> GetScheduleTableAsync(int @operator, string? lineSymbol, string stopCode)
    {
        var lineParam = lineSymbol != null ? $"&l={lineSymbol}" : string.Empty;
        var endpoint = $"/api/rest/v1/schedule/table?op={@operator}&s={stopCode}{lineParam}";
        return await httpClient.GetFromJsonAsync<ScheduleTable>(endpoint, JsonSerializerOptions.Web);
    }

    public async Task<GetRoutesResponseDto?> GetRoutesAsync(FindRoute findRoute)
    {
        var query = QueryStringBuilder.ToQueryString(findRoute);
        var endpoint = $"/api/rest/v2/routes?{query}";
        return await httpClient.GetFromJsonAsync<GetRoutesResponseDto>(endpoint, JsonSerializerOptions.Web);
    }
}