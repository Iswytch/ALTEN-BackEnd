
public interface IProductService
{
    IEnumerable<Product> GetAllProducts();
    Product? GetProductById(int id);
    void AddProduct(CreateProductDto product);
    void UpdateProduct(int id, UpdateProductDto product);
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

    public void AddProduct(CreateProductDto product) => _repository.AddProduct(product);

    public void UpdateProduct(int id, UpdateProductDto updateProduct){
        _repository.UpdateProduct(id, updateProduct);
    } 

    public void DeleteProduct(int id) => _repository.DeleteProduct(id);
}
