using System.ComponentModel.DataAnnotations;

namespace DTOs.Product;


public class CreateProductDto
{
    [StringLength(100, MinimumLength = 3)]
    public required string Name { get; set; } = String.Empty;

    [Range(0, double.MaxValue)]
    public required decimal Price { get; set; }

    [StringLength(500, MinimumLength = 0)]
    public required string Description { get; set; } = String.Empty;
}
