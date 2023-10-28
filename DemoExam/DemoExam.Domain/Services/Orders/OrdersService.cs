using DemoExam.Domain.Exceptions;
using DemoExam.Domain.Repositories;

namespace DemoExam.Domain.Services.Orders;

public class OrdersService : IOrdersService
{
    private readonly IOrdersRepository _ordersRepository;

    public OrdersService(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public Task<IEnumerable<Domain.Models.Order>> GetAllOrders() => _ordersRepository.GetAllAsync();
    public Task UpdateOrder(Domain.Models.Order order)
    {
        return _ordersRepository.UpdateOrder(order);
    }

    public Task<List<Models.Order>> GetOrdersForUser(int userId)
    {
        return _ordersRepository.GetOrdersForUser(userId);
    }

    public async Task<Models.Order> GetOrder(int orderId)
    {
        var order = await _ordersRepository.Get(orderId);
        return order ?? throw new NotFoundException();
    }
}