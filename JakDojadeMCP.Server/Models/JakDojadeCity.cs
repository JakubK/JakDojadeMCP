namespace JakDojadeMCP.Server.Models;

public record JakDojadeCity(string CityId, string Name, string NormalizedName, Coordinate CenterPoint, IEnumerable<Operator> Operators);
