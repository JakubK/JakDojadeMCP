using System.Net.Http.Headers;
using System.Text;
using JakDojadeMCP.Server;
using JakDojadeMCP.Server.Clients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMcpServer()
    .WithStdioServerTransport()
    .WithPrompts<JakDojadePrompts>()
    .WithResources<JakDojadeResources>();

string authToken = string.Empty;

if (args.Length >= 2) {
    var publicKey = args[0];
    var secretKey = args[1];
    authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{publicKey}:{secretKey}"));
}

builder.Services.AddHttpClient<JakDojadeClient>(x =>
{
    x.BaseAddress = new Uri("https://jakdojade.pl");
    x.DefaultRequestHeaders.Authorization = string.IsNullOrEmpty(authToken) ? null : new AuthenticationHeaderValue("Basic", authToken);
});

await builder.Build().RunAsync();