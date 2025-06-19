// See https://aka.ms/new-console-template for more information

using JakDojadeMCP.Server.Clients;
using JakDojadeMCP.Server.Models;

var client = new JakDojadeClient(new HttpClient
{
    BaseAddress = new Uri("https://jakdojade.pl")
});


var cities = await client.GetCitiesAsync();

var schedules = await client.GetScheduleTableAsync(1, "7", "ROSR-01");

var locations = await client.GetLocationsAsync("POZNAN", "rondo");


var routes = await client.GetRoutesAsync(
    new FindRoute(
        new SimpleCoordinate("52.23232","21.01599"),
        new SimpleCoordinate("52.26289", "20.98983"),
        null,
        null,
        null,
        null,
        1,
        1,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null
        )
    );

Console.WriteLine("END");