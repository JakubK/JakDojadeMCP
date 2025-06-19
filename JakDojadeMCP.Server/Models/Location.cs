namespace JakDojadeMCP.Server.Models;

public record Location(LocationType Type, Coordinate Coordinate, string Name, string? StopCode, bool? GroupStop, StopType? StopType, string? LinesText, string StreetName,  string CityName, int? StreetNumber, string? Category);