using System.Runtime.CompilerServices;
using System.Text.Json;

public interface IProductRepository
{
    IEnumerable<Product> GetAllProducts();
    Product? GetProductById(int id);
    void AddProduct(ProductDto product);
    void UpdateProduct(int id, ProductDto product);
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
  
  public void AddProduct(ProductDto createProduct){

    //Utiliser AutoMapper serait mieux
    var product = new Product
    {
        code = createProduct.code,
        name = createProduct.name,
        description = createProduct.description,
        image = createProduct.image,
        category = createProduct.category,
        price = createProduct.price,
        quantity = createProduct.quantity,
        internalReference = createProduct.internalReference,
        shellId = createProduct.shellId,
        inventoryStatus = createProduct.inventoryStatus,
        rating = createProduct.rating
    };

    product.id = _products.Any() ? _products.Max(p => p.id) + 1 : 1;
    product.createdAt = DateTime.UtcNow;
    product.updatedAt = DateTime.UtcNow;
    
    _products.Add(product);
    SaveChanges();
  }

  public void UpdateProduct(int id, ProductDto updateProduct) {

    var existingProduct = GetProductById(id);
    if (existingProduct is null) return;

    //Utiliser AutoMapper serait mieux
    existingProduct.code = updateProduct.code;
    existingProduct.name = updateProduct.name;
    existingProduct.description = updateProduct.description;
    existingProduct.image = updateProduct.image;
    existingProduct.category = updateProduct.category;
    existingProduct.price = updateProduct.price;
    existingProduct.quantity = updateProduct.quantity;
    existingProduct.internalReference = updateProduct.internalReference;
    existingProduct.shellId = updateProduct.shellId;
    existingProduct.inventoryStatus = updateProduct.inventoryStatus;
    existingProduct.rating = updateProduct.rating;

    existingProduct.updatedAt = DateTime.UtcNow;

    SaveChanges();
  }

  public void DeleteProduct(int id) {
    var product = GetProductById(id);
    if (product is null) return;
    _products.Remove(product);
    SaveChanges();
  }

  public void SaveChanges() {
    var json = JsonSerializer.Serialize(_products, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText(_filePath, json);
  }
}
