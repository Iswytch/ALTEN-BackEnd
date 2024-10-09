public interface IProductService
{
    IEnumerable<Product> GetAllProducts();
    Product? GetProductById(int id);
    void AddProduct(ProductDto product);
    void UpdateProduct(int id, ProductDto product);
    void DeleteProduct(int id);
}

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Product> GetAllProducts() => _repository.GetAllProducts();

    public Product? GetProductById(int id) => _repository.GetProductById(id);

    public void AddProduct(ProductDto product) => _repository.AddProduct(product);

    public void UpdateProduct(int id, ProductDto product) => _repository.UpdateProduct(id, product);

    public void DeleteProduct(int id) => _repository.DeleteProduct(id);
}
