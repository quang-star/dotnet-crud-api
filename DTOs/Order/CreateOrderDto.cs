using System.ComponentModel.DataAnnotations;

using DTOs.OrderDetail;

namespace DTOs.Order;

public class CreateOrderDto
{
    [Range(1, int.MaxValue)]
    public int UserId { get; set; }
    public List<CreateOrderDetailDto> OrderDetails { get; set; } = new List<CreateOrderDetailDto>();
}