using System.ComponentModel;
using System.Text.Json;
using JakDojadeMCP.Server.Clients;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;

namespace JakDojadeMCP.Server;

[McpServerResourceType]
public class JakDojadeResources
{
    [McpServerResource(UriTemplate = "https://jakdojade.pl/api/rest/v1/cities", MimeType = "text/plain"), Description("List all cities and their data such as operators and agglomeration names identified by normalizedName")]
    public static async Task<ResourceContents> ListCitiesAsync(JakDojadeClient client)
    {
        var cities = await client.GetCitiesAsync();
        return new TextResourceContents
        {
            Text = JsonSerializer.Serialize(cities),
            MimeType = "text/plain",
            Uri = "https://jakdojade.pl/api/rest/v1/cities",
        };
    }
}