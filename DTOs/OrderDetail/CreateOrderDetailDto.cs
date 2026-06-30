using System.ComponentModel.DataAnnotations;


namespace DTOs.OrderDetail;


public class CreateOrderDetailDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}