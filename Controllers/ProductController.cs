using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using DTOs.Product;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
    {
        var products = await _productService.GetAllProductAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto?>> GetProductById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<CreateProductDto?>> CreateProduct(CreateProductDto productDto)
    {
        var createdProduct = await _productService.CreateProductAsync(productDto);
        if (createdProduct == null)
        {
            return BadRequest();
        }
        return Ok(createdProduct);
    }               

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct(int id, UpdateProductDto productDto)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        await _productService.UpdateProductAsync(id, productDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        await _productService.DeleteProductAsync(id);
        return NoContent();
    }

}
