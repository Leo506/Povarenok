using DemoExam.Domain.Models;

namespace DemoExam.Domain.Repositories;

public interface IOrdersRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task UpdateOrder(Order order);
    Task<List<Order>> GetOrdersForUser(int userId);
    Task<Order?> Get(int orderId);
}