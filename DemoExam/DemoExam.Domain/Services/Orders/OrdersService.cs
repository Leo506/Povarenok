using DemoExam.Domain.Exceptions;
using DemoExam.Domain.Models;
using DemoExam.Domain.Repositories;
using DemoExam.Domain.Services.Products;

namespace DemoExam.Domain.Services.Orders;

public class OrdersService : IOrdersService
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductsService _productsService;

    public OrdersService(IOrdersRepository ordersRepository, IOrderRepository orderRepository,
        IProductsService productsService)
    {
        _ordersRepository = ordersRepository;
        _orderRepository = orderRepository;
        _productsService = productsService;
    }

    public Task<IEnumerable<Order>> GetAllOrders() => _ordersRepository.GetAllAsync();
    public Task UpdateOrder(Order order)
    {
        return _ordersRepository.UpdateOrder(order);
    }

    public Task<List<Order>> GetOrdersForUser(int userId)
    {
        return _ordersRepository.GetOrdersForUser(userId);
    }

    public async Task<Order> GetOrder(int orderId)
    {
        var order = await _ordersRepository.Get(orderId);
        return order ?? throw new EntityNotFoundException();
    }

    public async Task CreateNewOrder(int userId, int pickupPointId, Dictionary<string, int> products)
    {
        foreach (var (article, amount) in products)
        {
            if (amount <= 0)
                throw new InvalidProductsAmountInOrderException();

            if (await _productsService.Exists(article) is false)
                throw new EntityNotFoundException();
        }
        
        
        var random = new Random();
        var order = new Order()
        {
            UserId = userId,
            OrderDate = DateTime.Now,
            DeliveryDate = DateTime.Now.AddDays(random.Next(1, 4)),
            GetCode = random.Next(100, 1000),
            PickupPointId = pickupPointId,
            Status = OrderStatusConstants.NewOrder
        };
        order = await _orderRepository.CreateOrderAsync(order);
        foreach (var (article, amount) in products)
        {
            await _orderRepository.AddProductPositionToOrder(order.Id, article, amount);
        }
    }

    public async Task CancelOrder(int orderId)
    {
        var order = await GetOrder(orderId);
        await _orderRepository.RemoveOrder(order);
    }

    public async Task EditOrder(int orderId, Action<Order> updateAction, Dictionary<string, int> itemsToDelete)
    {
        var order = await GetOrder(orderId);
        updateAction(order);
        await _orderRepository.DeleteOrderItems(orderId, itemsToDelete);
    }
}