using System.Text.Json;
using System.Text.Json.Serialization;
using JakDojadeMCP.Server.Models;

namespace JakDojadeMCP.Server.Converters;

public class LocationTypeConverter : JsonConverter<LocationType>
{
    public override LocationType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => new(reader.GetString()!);

    public override void Write(Utf8JsonWriter writer, LocationType value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString());
}