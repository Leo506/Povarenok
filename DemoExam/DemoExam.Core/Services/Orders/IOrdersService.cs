namespace DemoExam.Core.Services.Orders;

public interface IOrdersService
{
    Task<IEnumerable<Domain.Models.Order>> GetAllOrders();
    Task UpdateOrder(Domain.Models.Order order);
}