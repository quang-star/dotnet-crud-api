
using System.ComponentModel.DataAnnotations;
using DTOs.OrderDetail;

namespace DTOs.Order;

public class UpdateOrderDto
{
    [Range(1, int.MaxValue)]
    public int UserId { get; set; }
    public List<UpdateOrderDetailDto> OrderDetails { get; set; } = new List<UpdateOrderDetailDto>();
}
