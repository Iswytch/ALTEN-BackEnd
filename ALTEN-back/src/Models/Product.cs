using System.Text.Json.Serialization;
using System.Text.Json;

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

public class Product {
    public int id { get; set; }
    public string code { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public string image { get; set; } = string.Empty;
    public string category { get; set; } = string.Empty;
    public decimal price { get; set; }
    public int quantity { get; set; }
    public string internalReference { get; set; } = string.Empty;
    public int shellId { get; set; }
    public string inventoryStatus { get; set; } = "INSTOCK";  // INSTOCK, LOWSTOCK, OUTOFSTOCK
    public int rating { get; set; }

    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime createdAt { get; set; }

    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime updatedAt { get; set; }
}
