using System.Text.Json;
using AutoMapper;

public interface IProductRepository
{
    IEnumerable<Product> GetAllProducts();
    Product? GetProductById(int id);
    void AddProduct(CreateProductDto product);
    void UpdateProduct(int id, UpdateProductDto product);
    void DeleteProduct(int id);
    void SaveChanges();  // Persist changes to the JSON file
}

public class JsonProductRepository: IProductRepository
{
  private List<Product> _products;

  private readonly IMapper _mapper;

  private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "products.json");

  public JsonProductRepository(IMapper mapper)
  {
    _mapper = mapper;

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

  public void AddProduct(CreateProductDto createProduct)
  {

    var product = _mapper.Map<Product>(createProduct);

    product.id = _products.Any() ? _products.Max(p => p.id) + 1 : 1;
    product.createdAt = DateTime.UtcNow;
    product.updatedAt = DateTime.UtcNow;
    
    _products.Add(product);
    SaveChanges();
  }

  public void UpdateProduct(int id, UpdateProductDto updateProduct) 
  {
    var existingProduct = GetProductById(id);
    if (existingProduct is null) return;
    _mapper.Map(updateProduct, existingProduct);
    existingProduct.updatedAt = DateTime.UtcNow;
    SaveChanges();
  }

  public void DeleteProduct(int id) 
  {
    var product = GetProductById(id);
    if (product is null) return;
    _products.Remove(product);
    SaveChanges();
  }

  public void SaveChanges() 
  {
    var json = JsonSerializer.Serialize(_products, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText(_filePath, json);
  }
}
