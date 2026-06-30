using DTOs.Product;
using Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<ProductDto>> GetAllProductAsync()
    {
        var products = await _productRepository.GetAllAsync();

        return products.Select(product => new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description
        }).ToList();
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return null;
        }

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description
        };
    }

    public async Task<CreateProductDto?> CreateProductAsync(CreateProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Price = productDto.Price,
            Description = productDto.Description,
            CreatedAt = DateTime.Now
        };

        var createdProduct = await _productRepository.AddAsync(product);

        return new CreateProductDto
        {
            Name = createdProduct.Name,
            Price = createdProduct.Price,
            Description = createdProduct.Description
        };
    }

    public async Task UpdateProductAsync(int id, UpdateProductDto productDto)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            throw new Exception("Product not found");
        }

        if (!string.IsNullOrEmpty(productDto.Name))
        {
            product.Name = productDto.Name;
        }

        product.Price = productDto.Price;

        if (!string.IsNullOrEmpty(productDto.Description))
        {
            product.Description = productDto.Description;
        }

        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            throw new Exception("Product not found");
        }

        await _productRepository.DeleteAsync(product);
    }
}
