using System.ComponentModel;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Server;

namespace JakDojadeMCP.Server;

[McpServerPromptType]
public class JakDojadePrompts
{

    [McpServerPrompt(Name = "find_route"), Description("Find route between 2 locations")]
    public static IEnumerable<ChatMessage> FindRoutePrompt(
        [Description("Start location")] string startLocation,
        [Description("End location")] string endLocation)
    {
        return [
            new ChatMessage(ChatRole.System, @"
                When asked about route between 2 locations:
                1. If cities for start and end locations are not known, ask for clarification
                2. Use list-cities tool to get json with all cities, extract matching agglomeration
                3. Use list-locations for each extracted agglomeration and associated location name as searchPhrase
                4. Extract both coordinates
                5. Use find-route tool with coordinates as params. Skip date and hour params unless you're provided with values.
            "),
            new ChatMessage(ChatRole.User, $"Find me a route from {startLocation} to {endLocation}")
        ];
    }
    
    [McpServerPrompt(Name = "query_scheudle"), Description("Asks about departures from given stop")]
    public static IEnumerable<ChatMessage> FindDepartures(
        [Description("Stop name")] string stopName,
        [Description("Line name")] string line)
    {
        return [
            new ChatMessage(ChatRole.System, @"
            When asked about departures of given line at given stop:
            1. If city is not known, ask about the city before proceeding
            2. Use list-cities tool to get json with all cities, extract agglomeration from matching city and collect all operatorIds for that city
            3. Use list-locations tool with agglomeration and given stop as searchPhrase, extract stopCode from the response
            4. Use list-departures tool for each operator, use extracted stopCode and line
"),
            new ChatMessage(ChatRole.User, $"Give me departures of line {line} at stop {stopName}")
        ];
    }
}