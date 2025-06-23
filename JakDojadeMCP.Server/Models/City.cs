namespace JakDojadeMCP.Server.Models;


public record City(string Name, string Agglomeration, IEnumerable<Operator> Operators);