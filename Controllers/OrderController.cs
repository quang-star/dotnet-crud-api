using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using DTOs.Order;
using Microsoft.AspNetCore.Authorization;
namespace Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto?>> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrder(CreateOrderDto orderDto)
    {
        await _orderService.CreateOrderAsync(orderDto);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateOrder(int id, UpdateOrderDto orderDto)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrder(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        await _orderService.DeleteOrderAsync(order);
        return NoContent();
    }
}