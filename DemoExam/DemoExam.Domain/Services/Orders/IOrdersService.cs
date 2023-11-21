﻿namespace DemoExam.Domain.Services.Orders;

public interface IOrdersService
{
    Task<IEnumerable<Domain.Models.Order>> GetAllOrders();
    Task UpdateOrder(Domain.Models.Order order);
    Task<List<Models.Order>> GetOrdersForUser(int userId);
    Task<Models.Order> GetOrder(int orderId);
    Task CreateNewOrder(int userId, int pickupPointId, Dictionary<string, int> products);
    Task CancelOrder(int orderId);
    Task EditOrder(int orderId, int? pickupPointId, List<Tuple<string, int>> orderItemsToDelete);
}