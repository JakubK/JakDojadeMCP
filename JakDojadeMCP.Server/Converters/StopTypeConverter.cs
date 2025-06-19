using System.Text.Json;
using System.Text.Json.Serialization;
using JakDojadeMCP.Server.Models;

namespace JakDojadeMCP.Server.Converters;

public class StopTypeConverter : JsonConverter<StopType>
{
    public override StopType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => new(reader.GetString()!); // Or match against known instances

    public override void Write(Utf8JsonWriter writer, StopType value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString());
}