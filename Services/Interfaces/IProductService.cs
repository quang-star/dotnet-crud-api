using DTOs.Product;

namespace Services.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetAllProductAsync();
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<CreateProductDto?> CreateProductAsync(CreateProductDto productDto);
    Task UpdateProductAsync(int id, UpdateProductDto productDto);
    Task DeleteProductAsync(int id);
}

