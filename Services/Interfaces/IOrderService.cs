using Models;
using DTOs.Order;
namespace Services.Interfaces;

public interface IOrderService
{
    Task<List<OrderDto>> GetAllOrdersAsync();
    Task<OrderDto?> GetOrderByIdAsync(int id);
    Task CreateOrderAsync(CreateOrderDto order);
    Task UpdateOrderAsync(int id, UpdateOrderDto order);
    Task DeleteOrderAsync(OrderDto order);
}
