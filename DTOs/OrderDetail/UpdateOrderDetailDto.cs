using System.ComponentModel.DataAnnotations;

namespace DTOs.OrderDetail;

public class UpdateOrderDetailDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}