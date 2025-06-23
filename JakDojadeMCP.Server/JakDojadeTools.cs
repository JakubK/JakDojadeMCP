using System.ComponentModel;
using System.Text.Json;
using JakDojadeMCP.Server.Clients;
using JakDojadeMCP.Server.Models;
using ModelContextProtocol.Server;

namespace JakDojadeMCP.Server;

[McpServerToolType]
public class JakDojadeTools
{
    [McpServerTool(Name = "list-cities"), Description("List all cities, their operators and agglomeration names")]
    public static async Task<string> ListCitiesAsync(JakDojadeClient client)
    {
        var jdCities = await client.GetCitiesAsync();
        var cities = jdCities?.Cities.Select(x => new City(x.Name, x.NormalizedName, x.Operators));
        
        return JsonSerializer.Serialize(cities);
    }

    [McpServerTool(Name = "list-locations"), Description("List locations by given searchPhrase and agglomeration. Agglomeration should be fetched first using list-cities tool")]
    public static async Task<string> ListLocationsAsync(JakDojadeClient client,
        [Description("Agglomeration name. If not provided directly, should be retrieved from list-cities tool")] string agglomeration,
        [Description("Search phrase for the location")] string searchPhrase)
    {
        var locations = await client.GetLocationsAsync(agglomeration, searchPhrase);
        return JsonSerializer.Serialize(locations);
    }

    [McpServerTool(Name = "list-departures"), Description("List departures for given stopCode, lineSymbol and operatorId. stopCode can be obtained using list-locations and operatorId can be obtained from list-cities tool")]
    public static async Task<string> ListDeparturesAsync(JakDojadeClient client,
        [Description("Required number which is identifying the operator. Should be retrieved from list-cities tool")] int operatorId,
        [Description("Required stopCode which is identifying the stop. Should be retrieved from list-locations tool")] string stopCode,
        [Description("Optional line symbol. Omitted will return all departures from stop")] string? lineSymbol)
    {
        var obj = await client.GetScheduleTableAsync(operatorId, lineSymbol, stopCode);
        return JsonSerializer.Serialize(obj);
    }
    
    [McpServerTool(Name = "find-route"), Description("Finds feasible route between start and end points. Accepts variety of configuration params")]
    public static async Task<string> ListRoutesAsync(JakDojadeClient client,
        [Description("Longitude of starting point. If not given directly, should be obtained via list-locations tool")] string startPointLon,
        [Description("Latitude of starting point. If not given directly, should be obtained via list-locations tool")] string startPointLat,
        [Description("Longitude of end point. If not given directly, should be obtained via list-locations tool")] string endPointLon,
        [Description("Latitude of end point. If not given directly, should be obtained via list-locations tool")] string endPointLat,
        [Description("Agglomeration to be used. Skipped will use agglomeration closest to the start point")] string? agglomeration = null,
        [Description("Date to start search in DD.MM.YY format. Omitted will use today as a date")] string? date = null,
        [Description("Hour to start search in HH:MM format. Omitted will use now as hour")] string? hour = null,
        [Description("Mode of routing. Allowed values: conveniet,optimal,hurry. Will use 'optimal' if omitted")] string? type = null,
        [Description("If routes with less changes should be preferred. Defaults to false if omitted")] bool? avoidChanges = null,
        [Description("If routes with less busses should be preferred. Defaults to false if omitted")] bool? avoidBuses = null,
        [Description("Comma separated list of means of transport which should be prohibited. Allowed values are [bus, tram, subway, train, microbus, trolleybus, waterTram]. When omitted sets to empty string")] string? prohibitedVehicles = null,
        [Description("Comma separated list of ids of operators which should be prohibited. When omitted sets to empty string")] string? prohibitedOperators = null,
        [Description("If express lines should be omitted. Defaults to false if omitted")] bool? avoidExpresses = null,
        [Description("If zone lines should be avoided. Defaults to false if omitted")] bool? avoidZonal = null,
        [Description("If routes with only low floor vehicles should be accepted. Defaults to false if omitted")] bool? onlyLowFloor = null,
        [Description("Comma separated list of symbols of lines which should be omitted. Skips nothing if this param is ignored")] string? prohibitedLines = null,
        [Description("Comma separated list of lines which are preferred. Defaults to empty string")] string? preferLines = null,
        [Description("The amount of routes to find. Upper bound is 3. Defaults to 1 if skipped")] int? routeCount = 1
    )
    {
        var findRoute = new FindRoute
        (
            StartPointLatitude: startPointLat,
            StartPointLongitude: startPointLon,
            EndPointLatitude: endPointLat,
            EndPointLongitude: endPointLon,
            Agglomeration: agglomeration,
            Date: date,
            Hour: hour,
            Type: type,
            AvoidChanges: avoidChanges,
            AvoidBuses: avoidBuses,
            AvoidExpresses: avoidExpresses,
            AvoidZonal: avoidZonal,
            OnlyLowFloor: onlyLowFloor,
            RoadsOn: null,
            ProhibitedVehicles: prohibitedVehicles,
            ProhibitedOperators: prohibitedOperators,
            ProhibitedLines: prohibitedLines,
            PreferLines: preferLines,
            Test: null,
            IsArrival: false,
            RouteCount: routeCount,
            RouteIndex: 1,
            IncludeRoadCoordinates: null
        );
        
        var obj = await client.GetRoutesAsync(findRoute);
        var descriptions = obj.Routes.Select(x => x.Description);
        return JsonSerializer.Serialize(descriptions);
    }
}