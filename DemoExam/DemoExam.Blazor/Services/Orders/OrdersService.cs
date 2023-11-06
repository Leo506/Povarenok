using Arbus.Network;
using DemoExam.Blazor.Network.Endpoints;
using DemoExam.Blazor.Services.Basket;
using DemoExam.Blazor.Shared.Dto.Requests;
using DemoExam.Blazor.Shared.Dto.Responses;

namespace DemoExam.Blazor.Services.Orders;

public class OrdersService : IOrdersService
{
    private readonly IBasketService _basketService;
    private readonly HttpClientContext _clientContext;

    public OrdersService(IBasketService basketService, HttpClientContext clientContext)
    {
        _basketService = basketService;
        _clientContext = clientContext;
    }

    public Task<List<OrderShort>> GetUserOrders(int userId) => _clientContext.RunEndpoint(new UserOrdersEndpoint(userId));

    public Task<Order> GetOrder(int orderId) => _clientContext.RunEndpoint(new OrderEndpoint(orderId));

    public Task CreateNew(int pickupPointId)
    {
        var dto = new NewOrder()
        {
            PickupPointId = pickupPointId,
            Products = _basketService.GetAll().ToDictionary(x => x.Key.ArticleNumber, x => x.Value)
        };
        return _clientContext.RunEndpoint(new NewOrderEndpoint(dto));
    }
}