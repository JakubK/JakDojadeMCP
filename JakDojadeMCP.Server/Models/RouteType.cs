namespace JakDojadeMCP.Server.Models;


public sealed class RouteType(string value)
{
    private string Value { get; } = value;
    
    public override string ToString() => Value;
    
    
    public static readonly RouteType Convenient = new("convenient");
    public static readonly RouteType Optimal = new("optimal");
    public static readonly RouteType Hurry = new("hurry"); 
}