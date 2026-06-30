using DTOs.User;
using DTOs.OrderDetail;
namespace DTOs.Order;


public class OrderDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public UserDto User { get; set; } = null!;
    public List<OrderDetailDto> OrderDetails { get; set; } = new List<OrderDetailDto>();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}