namespace DemoExam.Core.Repositories;

public interface IOrderRepository
{
    Task<Order> CreateOrderAsync(Order order);

    Task AddProductPositionToOrder(int orderId, string productId, int amount);
    Task<int> GetLastOrderId();
}