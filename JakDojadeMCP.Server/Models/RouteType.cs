using System.Text.Json.Serialization;
using JakDojadeMCP.Server.Converters;

namespace JakDojadeMCP.Server.Models;


[JsonConverter(typeof(RouteTypeConverter))]
public sealed class RouteType(string value)
{
    private string Value { get; } = value;
    
    public override string ToString() => Value;
    
    
    public static readonly RouteType Convenient = new("convenient");
    public static readonly RouteType Optimal = new("optimal");
    public static readonly RouteType Hurry = new("hurry"); 
}