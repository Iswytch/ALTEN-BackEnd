public interface IProductService
{
    IEnumerable<Product> GetAllProducts();
    Product? GetProductById(int id);
    void AddProduct(Product product);
    void UpdateProduct(Product product);
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

    public void AddProduct(Product product) => _repository.AddProduct(product);

    public void UpdateProduct(Product product) => _repository.UpdateProduct(product);

    public void DeleteProduct(int id) => _repository.DeleteProduct(id);
}
