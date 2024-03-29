﻿using DemoExam.Domain.Models;

namespace DemoExam.Domain.Services.Orders;

public interface IOrdersService
{
    Task<IEnumerable<Order>> GetAllOrders();
    Task<List<Order>> GetOrdersForUser(int userId);
    Task<Order> GetOrder(int orderId);
    Task CreateNewOrder(int userId, int pickupPointId, Dictionary<string, int> products);
    Task CancelOrder(int orderId);
    Task EditOrder(int orderId, Action<Order> updateAction, Dictionary<string, int> itemsToDelete);
}