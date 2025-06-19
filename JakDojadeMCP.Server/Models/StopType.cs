using System.Text.Json.Serialization;
using JakDojadeMCP.Server.Converters;

namespace JakDojadeMCP.Server.Models;

[JsonConverter(typeof(StopTypeConverter))]
public sealed class StopType(string value)
{
    private string Value { get; } = value;
    
    public override string ToString() => Value;
    
    public static readonly StopType Bus = new("bus");
    public static readonly StopType Tram = new("tram");
    public static readonly StopType TramBus = new("trambus");
    public static readonly StopType Metro = new("metro");
    public static readonly StopType Train = new("train");
}