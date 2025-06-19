using System.ComponentModel;
using System.Text.Json;
using JakDojadeMCP.Server.Clients;
using JakDojadeMCP.Server.Models;
using ModelContextProtocol.Server;

namespace JakDojadeMCP.Server;

[McpServerResourceType]
public class JakDojadeResources
{
    [McpServerResource(Name = "List Cities and their data", MimeType = "text/plain"), Description("List all cities and their data such as operators and agglomeration names identified by normalizedName")]
    public static async Task<string> ListCitiesAsync(JakDojadeClient client)
    {
        var cities = await client.GetCitiesAsync();
        return JsonSerializer.Serialize(cities);
    }
    
    [McpServerResource(UriTemplate = "jd://list/locations", Name = "List locations", MimeType = "text/plain"), Description("List locations by given searchphrase and agglomeration")]
    public static async Task<string> ListLocationsAsync(JakDojadeClient client,
        [Description("Agglomeration name")] string agglomeration,
        [Description("Search phrase for the location")] string searchPhrase)
    {
        var locations = await client.GetLocationsAsync(agglomeration, searchPhrase);
        return JsonSerializer.Serialize(locations);
    }

    [McpServerResource(UriTemplate = "jd://list/departures", Name = "List Departures", MimeType = "text/plain"), Description("List departures for given stopCode, lineSymbol and operatorId")]
    public static async Task<string> ListDeparturesAsync(JakDojadeClient client,
        [Description("Required number which is identifying the operator")] int operatorId,
        [Description("Required stopCode which is identifying the stop")] string stopCode,
        [Description("Optional line symbol. Omitted will return all departures from stop")] string? lineSymbol)
    {
        var obj = await client.GetScheduleTableAsync(operatorId, lineSymbol, stopCode);
        return JsonSerializer.Serialize(obj);
    }
    
    [McpServerResource(UriTemplate = "jd://list/routes", Name = "Find route", MimeType = "text/plain"), Description("Finds feasible route between start and end points. Accepts variety of configuration params")]
    public static async Task<string> ListRoutesAsync(JakDojadeClient client,
        [Description("Longitude of starting point")] string startPointLon,
        [Description("Latitude of starting point")] string startPointLat,
        [Description("Longitude of end point")] string endPointLon,
        [Description("Latitude of end point")] string endPointLat,
        [Description("Agglomeration also known as normalizedName. Skipped will use agglomeration closest to the start point")] string? agglomeration,
        [Description("Date to start search in DD.MM.YY format. Omitted will use today as a date")] string? date,
        [Description("Hour to start search in HH:MM format. Omitted will use now as hour")] string? hour,
        [Description("Mode of routing. Allowed values: conveniet,optimal,hurry. Will use 'optimal' if omitted")] string? type,
        [Description("If routes with less changes should be preferred. Defaults to false if omitted")] bool? avoidChanges,
        [Description("If routes with less busses should be preferred. Defaults to false if omitted")] bool? avoidBuses,
        [Description("Comma separated list of means of transport which should be prohibited. Allowed values are [bus, tram, subway, train, microbus, trolleybus, waterTram]. When omitted sets to empty string")] string? prohibitesVehicles,
        [Description("Comma separated list of ids of operators which should be prohibited. When omitted sets to empty string")] string? prohibitesOperators,
        [Description("If express lines should be omitted. Defaults to false if omitted")] bool? avoidExpresses,
        [Description("If zone lines should be avoided. Defaults to false if omitted")] bool? avoidZonal,
        [Description("If routes with only low floor vehicles should be accepted. Defaults to false if omitted")] bool? onlyLowFloor,
        [Description("Comma separated list of symbols of lines which should be omitted. Skips nothing if this param is ignored")] string? prohibitedLines,
        [Description("Comma separated list of lines which are preferred. Defaults to empty string")] string? preferLines
    )
    {
        var proVehiclesParam = prohibitesVehicles == null ? [] : prohibitesVehicles.Split(',');
        var proOperatorsParam = prohibitesOperators == null ? [] : prohibitesOperators.Split(',');
        var proLinesParam = prohibitedLines == null ? [] : prohibitedLines.Split(',');
        
        var preLinesParam = preferLines == null ? [] : preferLines.Split(',');

        var findRoute = new FindRoute
        (
            StartPoint: new SimpleCoordinate(startPointLat, startPointLon),
            EndPoint: new SimpleCoordinate(endPointLat, endPointLon),
            Agglomeration: agglomeration,
            Date: date,
            Hour: hour,
            Type: type == null ? null : new RouteType(type),
            AvoidChanges: avoidChanges,
            AvoidBuses: avoidBuses,
            AvoidExpresses: avoidExpresses,
            AvoidZonal: avoidZonal,
            OnlyLowFloor: onlyLowFloor,
            RoadsOn: null,
            ProhibitedVehicles: proVehiclesParam,
            ProhibitedOperators: proOperatorsParam,
            ProhibitedLines: proLinesParam,
            PreferLines: preLinesParam,
            Test: null,
            IsArrival: false,
            RouteCount: null,
            RouteIndex: null,
            IncludeRoadCoordinates: null
        );
        
        var obj = await client.GetRoutesAsync(findRoute);
        var descriptions = obj.Routes.Select(x => x.Description);
        return JsonSerializer.Serialize(descriptions);
    }
}