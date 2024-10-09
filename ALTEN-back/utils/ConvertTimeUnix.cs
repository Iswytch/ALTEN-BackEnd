using System.Text.Json;
using System.Text.Json.Serialization;

public class UnixDateTimeConverter : JsonConverter<DateTime>
{
    // Convertit le timestamp Unix en DateTime lors de la lecture du JSON
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var timestamp = reader.GetInt64(); // Récupère le timestamp Unix
        return DateTimeOffset.FromUnixTimeMilliseconds(timestamp).UtcDateTime; // Convertit en DateTime
    }

    // Convertit le DateTime en timestamp Unix lors de la sérialisation en JSON
    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        var unixTimestamp = new DateTimeOffset(value).ToUnixTimeMilliseconds(); // Convertit en Unix timestamp
        writer.WriteNumberValue(unixTimestamp); // Écrit la valeur dans le JSON
    }
}