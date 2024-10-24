using AutoMapper;

public interface IProductRepositoryDb
{
    IEnumerable<Product> GetAllProducts();
    Product? GetProductById(int id);
    void AddProduct(CreateProductDto product);
    void UpdateProduct(int id, UpdateProductDto product);
    void DeleteProduct(int id);
}

public class DbProductRepository: IProductRepositoryDb
{
  private readonly AppDbContext _context;
  private readonly IMapper _mapper;

  public DbProductRepository(IMapper mapper, AppDbContext context)
  {
    _mapper = mapper;
    _context = context;
  }

  public IEnumerable<Product> GetAllProducts() => _context.Products.ToList();

  public Product? GetProductById(int id) => _context.Products.FirstOrDefault(p => p.id == id);

  public void AddProduct(CreateProductDto createProduct)
  {

    Product product = _mapper.Map<Product>(createProduct);

    product.createdAt = DateTime.UtcNow;
    product.updatedAt = DateTime.UtcNow;
    
    _context.Products.Add(product);
    _context.SaveChanges();
  }

  public void UpdateProduct(int id, UpdateProductDto updateProduct) 
  {
    //Ce code motifie directement l'objet
    Product? existingProduct = GetProductById(id);
    if (existingProduct is null) return;
    _mapper.Map(updateProduct, existingProduct);
    existingProduct.updatedAt = DateTime.UtcNow;

    //_context.Products.Update(existingProduct);
    _context.SaveChanges();
  }

  public void DeleteProduct(int id) 
  {
    Product? product = GetProductById(id);
    if (product is null) return;
    _context.Products.Remove(product);
    _context.SaveChanges();
  }
}
