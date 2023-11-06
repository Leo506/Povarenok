using DemoExam.Blazor.Shared.Dto.Responses;

namespace DemoExam.Blazor.Services.Orders;

public interface IOrdersService
{
    Task<List<OrderShort>> GetUserOrders(int userId);

    Task<Order> GetOrder(int orderId);

    Task CreateNew(int pickupPointId);
}