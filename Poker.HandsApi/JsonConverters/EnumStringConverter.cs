namespace Poker.HandsApi.JsonConverters;

using System.Text.Json;
using System.Text.Json.Serialization;

public class EnumStringConverter<T> : JsonConverter<T> where T : Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException($"Unexpected token type '{reader.TokenType}' when parsing enum.");
        }

        string enumValue = reader.GetString()!;
        return (T) Enum.Parse(typeof(T), enumValue);
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
