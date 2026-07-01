using Models;
using DTOs.Order;
using DTOs.User;
using DTOs.OrderDetail;
using Repositories.Interfaces;
using Services.Interfaces;
using FluentValidation;
namespace Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IValidator<CreateOrderDto> _createOrderValidator;
    private readonly IValidator<UpdateOrderDto> _updateOrderValidator;
    public OrderService(IOrderRepository orderRepository, IValidator<CreateOrderDto> createOrderValidator, IValidator<UpdateOrderDto> updateOrderValidator)
    {
        _orderRepository = orderRepository;
        _createOrderValidator = createOrderValidator;
        _updateOrderValidator = updateOrderValidator;
    }

    public async Task<List<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        if (orders == null || !orders.Any())
        {
            throw new Exception("No orders found.");
        }
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
        if (order == null)
        {
            throw new Exception($"Order with id {id} not found.");
        }

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

    public async Task CreateOrderAsync(CreateOrderDto orderDto)
    {

        var validationResult = await _createOrderValidator.ValidateAsync(orderDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
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

        await _orderRepository.AddAsync(order);

    }

    public async Task UpdateOrderAsync(int id, UpdateOrderDto orderDto)
    {
        var validationResult = await _updateOrderValidator.ValidateAsync(orderDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
        {
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
        if (order == null)
        {
            throw new Exception($"Order with id {orderDto.Id} not found.");
        }

        await _orderRepository.DeleteAsync(order);
    }
}