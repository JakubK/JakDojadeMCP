using System.Text.Json;
using System.Text.Json.Serialization;
using JakDojadeMCP.Server.Models;

namespace JakDojadeMCP.Server.Converters;

public class RouteTypeConverter : JsonConverter<RouteType>
{
    public override RouteType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => new(reader.GetString()!);

    public override void Write(Utf8JsonWriter writer, RouteType value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString());
}