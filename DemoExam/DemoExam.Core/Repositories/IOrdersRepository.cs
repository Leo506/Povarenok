namespace DemoExam.Core.Repositories;

public interface IOrdersRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task UpdateOrder(Order order);
}