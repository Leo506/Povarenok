using DemoExam.Blazor.Shared;

namespace DemoExam.Blazor.Services.Orders;

public interface IOrdersService
{
    Task<List<OrderShortDto>> GetUserOrders(int userId);

    Task<OrderDto> GetOrder(int orderId);

    Task CreateNew(int pickupPointId);
}