using DemoExam.Domain.Models;

namespace DemoExam.Domain.Repositories;

public interface IOrderRepository
{
    Task<Order> CreateOrderAsync(Order order);

    Task AddProductPositionToOrder(int orderId, string productId, int amount);
    Task<int> GetLastOrderId();
    Task RemoveOrder(Order order);
    Task DeleteOrderItems(int orderId, Dictionary<string, int> orderItemsToDelete);
}