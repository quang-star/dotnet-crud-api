using System.ComponentModel.DataAnnotations;

namespace DTOs.Product;


public class CreateProductDto
{
   
    public string Name { get; set; } = String.Empty;

    public decimal Price { get; set; }

    public string Description { get; set; } = String.Empty;
}
