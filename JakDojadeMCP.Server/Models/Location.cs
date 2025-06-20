namespace JakDojadeMCP.Server.Models;

public record Location(string? Type, Coordinate Coordinate, string Name, string? StopCode, bool? GroupStop, string? StopType, string? LinesText, string StreetName,  string CityName, int? StreetNumber, string? Category);