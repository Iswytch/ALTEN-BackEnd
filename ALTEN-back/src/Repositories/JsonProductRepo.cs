using System.Text.Json;

public interface IProductRepository
{
    IEnumerable<Product> GetAllProducts();
    Product? GetProductById(int id);
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(int id);
    void SaveChanges();  // Persist changes to the JSON file
}

public class JsonProductRepository: IProductRepository
{
  private List<Product> _products;

  private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "products.json");

  public JsonProductRepository()
  {
    if (File.Exists(_filePath))
    {
      var json = File.ReadAllText(_filePath);
      _products = JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
    }
    else
    {
      throw new FileNotFoundException($"Le fichier {_filePath} n'a pas été trouvé.");
    }
  }

  public IEnumerable<Product> GetAllProducts() => _products;

  public Product? GetProductById(int id) => _products.FirstOrDefault(p => p.id == id);

  public void AddProduct(Product product){
    product.id = _products.Any() ? _products.Max(p => p.id) + 1 : 1;
    product.createdAt = DateTime.UtcNow;
    product.updatedAt = DateTime.UtcNow;
    _products.Add(product);
    SaveChanges();
  }

  public void UpdateProduct(Product product) {
    var existing = GetProductById(product.id);
    if (existing is null) return;

    existing.code = product.code;
    existing.name = product.name;
    existing.description = product.description;
    existing.image = product.image;
    existing.category = product.category;
    existing.price = product.price;
    existing.quantity = product.quantity;
    existing.internalReference = product.internalReference;
    existing.shellId = product.shellId;
    existing.inventoryStatus = product.inventoryStatus;
    existing.rating = product.rating;
    existing.updatedAt = DateTime.UtcNow;

    SaveChanges();
  }

  public void DeleteProduct(int id) {
    var product = GetProductById(id);
    if (product is not null)
    {
      _products.Remove(product);
      SaveChanges();
    }
  }

  public void SaveChanges() {
    var json = JsonSerializer.Serialize(_products, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText(_filePath, json);
  }
}
