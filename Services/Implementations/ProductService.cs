using DTOs.Product;
using Models;
using Repositories.Interfaces;
using Services.Interfaces;
using FluentValidation;
using Validators.Product;
namespace Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IValidator<CreateProductDto> _createProductValidator;
    private readonly IValidator<UpdateProductDto> _updateProductValidator;

    public ProductService(IProductRepository productRepository, IValidator<CreateProductDto> createProductValidator, IValidator<UpdateProductDto> updateProductValidator)
    {
        _productRepository = productRepository;
        _createProductValidator = createProductValidator;
        _updateProductValidator = updateProductValidator;
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
            throw new Exception("Product not found");
        }

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description
        };
    }

    public async Task CreateProductAsync(CreateProductDto productDto)
    {
        var validationResult = await _createProductValidator.ValidateAsync(productDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var product = new Product
        {
            Name = productDto.Name,
            Price = productDto.Price,
            Description = productDto.Description,
            CreatedAt = DateTime.Now
        };

        await _productRepository.AddAsync(product);
    }

    public async Task UpdateProductAsync(int id, UpdateProductDto productDto)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            throw new Exception("Product not found");
        }

        var validationResult = await _updateProductValidator.ValidateAsync(productDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
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
