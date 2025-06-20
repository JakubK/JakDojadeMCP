using System.ComponentModel;
using System.Text.Json;
using JakDojadeMCP.Server.Clients;
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
}