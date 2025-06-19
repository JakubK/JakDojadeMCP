namespace JakDojadeMCP.Server.Models;

public record City(string Name, string NormalizedName, Coordinate CenterPoint, IEnumerable<Operator> Operators);