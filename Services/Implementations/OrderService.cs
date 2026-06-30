using Models;
using DTOs.Order;
using DTOs.User;
using DTOs.OrderDetail;
using Repositories.Interfaces;
using Services.Interfaces;
namespace Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<List<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return orders.Select(order => new OrderDto
        {
            Id = order.Id,
            UserId = order.UserId,
            User = new UserDto
            {
                Id = order.User.Id,
                Name = order.User.Name,
                Email = order.User.Email
            },
            OrderDetails = order.OrderDetails.Select(orderDetail => new OrderDetailDto
            {
                Id = orderDetail.Id,
                ProductId = orderDetail.ProductId,
                Quantity = orderDetail.Quantity
            }).ToList()
        }).ToList();
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null) return null;

        return new OrderDto
        {
            Id = order.Id,
            UserId = order.UserId,
            User = new UserDto
            {
                Id = order.User.Id,
                Name = order.User.Name,
                Email = order.User.Email
            },
            OrderDetails = order.OrderDetails.Select(orderDetail => new OrderDetailDto
            {
                Id = orderDetail.Id,
                ProductId = orderDetail.ProductId,
                Quantity = orderDetail.Quantity
            }).ToList()
        };
    }

    public async Task<CreateOrderDto> CreateOrderAsync(CreateOrderDto orderDto)
    {
        var order = new Order
        {
            UserId = orderDto.UserId,
           // CreatedAt = DateTime.Now,
            OrderDetails = orderDto.OrderDetails.Select(orderDetail => new OrderDetail
            {
                ProductId = orderDetail.ProductId,
                Quantity = orderDetail.Quantity
            }).ToList()
        };

        var createdOrder = await _orderRepository.AddAsync(order);

        return new CreateOrderDto
        {
            UserId = createdOrder.UserId,
            OrderDetails = createdOrder.OrderDetails.Select(orderDetail => new CreateOrderDetailDto
            {
                ProductId = orderDetail.ProductId,
                Quantity = orderDetail.Quantity
            }).ToList()
        };
    }

    public async Task UpdateOrderAsync(int id, UpdateOrderDto orderDto)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if(order == null) {
            throw new Exception($"Order with id {id} not found.");
        }

        order.UserId = orderDto.UserId;
        order.OrderDetails = orderDto.OrderDetails.Select(orderDetail => new OrderDetail
        {
            ProductId = orderDetail.ProductId,
            Quantity = orderDetail.Quantity
        }).ToList();

        await _orderRepository.UpdateAsync(order);

    }

   

    public async Task DeleteOrderAsync(OrderDto orderDto)
    {
        var order = await _orderRepository.GetByIdAsync(orderDto.Id);
        if(order == null) {
            throw new Exception($"Order with id {orderDto.Id} not found.");
        }

        await _orderRepository.DeleteAsync(order);
    }
}