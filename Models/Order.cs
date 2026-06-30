namespace Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public decimal TotalAmount { get; set; }

    public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

}