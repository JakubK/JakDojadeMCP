namespace JakDojadeMCP.Server.Models;

public sealed class LocationType(string value)
{
    private string Value { get; } = value;
    
    public override string ToString() => Value;
    
    
    public static readonly LocationType Stop = new("stop");
    public static readonly LocationType Street = new("street");
    public static readonly LocationType Poi = new("poi");
}