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
            new ChatMessage(ChatRole.User, $"Find me a route from {startLocation} to {endLocation}")
        ];
    }
    
    [McpServerPrompt(Name = "query_scheudle"), Description("Asks about departures from given stop")]
    public static IEnumerable<ChatMessage> FindDepartures(
        [Description("Stop name")] string stopName,
        [Description("Line name")] string line)
    {
        return [
            new ChatMessage(ChatRole.User, $"Give me departures of line {line} at stop {stopName}")
        ];
    }
}