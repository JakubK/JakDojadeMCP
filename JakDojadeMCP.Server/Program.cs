using JakDojadeMCP.Server.Clients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMcpServer()
    .WithStdioServerTransport();
    
builder.Services.AddHttpClient<JakDojadeClient>(x =>
{
    x.BaseAddress = new Uri("https://jakdojade.pl");
});

await builder.Build().RunAsync();