using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        var products = _service.GetAllProducts();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult GetProductById(int id)
    {
        var product = _service.GetProductById(id);
        if (product is null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public IActionResult AddProduct([FromBody] ProductDto product)
    {
        _service.AddProduct(product);
        return Created($"/Product", product);
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] ProductDto product)
    {
        var existing = _service.GetProductById(id);
        if (existing is null) return NotFound();
        
        _service.UpdateProduct(id, product);
        return Ok(product);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var existing = _service.GetProductById(id);
        if (existing is null) return NotFound();

        _service.DeleteProduct(id);
        return Ok();
    }
}
