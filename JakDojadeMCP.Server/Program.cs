using System.Net.Http.Headers;
using System.Text;
using JakDojadeMCP.Server;
using JakDojadeMCP.Server.Clients;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var transportMode = Environment.GetEnvironmentVariable("TransportMode") ?? "stdio";
var publicKey = Environment.GetEnvironmentVariable("PublicKey") ?? string.Empty;
var secretKey = Environment.GetEnvironmentVariable("SecretKey") ?? string.Empty;

string? GetAuthToken()
{
    if (!string.IsNullOrEmpty(publicKey))
        return Convert.ToBase64String(Encoding.ASCII.GetBytes($"{publicKey}:{secretKey}"));
    return null;
}

void ConfigureCommonServices(IServiceCollection services)
{
    services.AddMcpServer()
        .WithStdioServerTransport()
        .WithHttpTransport()
        .WithPrompts<JakDojadePrompts>()
        .WithResources<JakDojadeResources>()
        .WithTools<JakDojadeTools>();

    string? authToken = GetAuthToken();
    
    services.AddHttpClient<JakDojadeClient>(client =>
    {
        client.BaseAddress = new Uri("https://jakdojade.pl");
        if (!string.IsNullOrEmpty(authToken))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
        }
    });
}

if (transportMode == "stdio")
{
    var builder = Host.CreateApplicationBuilder(args);
    builder.Logging.AddConsole(consoleLogOptions =>
    {
        // Configure all logs to go to stderr
        consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
    });
    ConfigureCommonServices(builder.Services);
    var app = builder.Build();
    await app.RunAsync();
}
else
{
    var builder = WebApplication.CreateBuilder(args);
    ConfigureCommonServices(builder.Services);
    var app = builder.Build();
    app.MapMcp();
    await app.RunAsync();
}