using System.Text.Json.Serialization;

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
    public string inventoryStatus { get; set; } = string.Empty;
    public int rating { get; set; }

    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime createdAt { get; set; }

    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime updatedAt { get; set; }
}
