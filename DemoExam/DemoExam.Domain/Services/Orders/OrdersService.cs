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
}