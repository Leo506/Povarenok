using DemoExam.Core.Extensions;
using DemoExam.Core.ObservableObjects;
using DemoExam.Domain.Repositories;
using DemoExam.Domain.Services.Order;

namespace DemoExam.Core.Services.ViewModelServices.Order;

public class OrderViewModelService : IOrderViewModelService
{
    private readonly IOrderService _orderService;
    private readonly IProductRepository _productRepository;
    private readonly IPickupPointRepository _pickupPointRepository;

    public OrderViewModelService(IOrderService orderService, IProductRepository productRepository,
        IPickupPointRepository pickupPointRepository)
    {
        _orderService = orderService;
        _productRepository = productRepository;
        _pickupPointRepository = pickupPointRepository;
    }

    public async Task<IEnumerable<ObservableOrder>> GetProductsInOrder()
    {
        var orders = new List<ObservableOrder>();
        foreach (var (productId, amount) in _orderService.GetOrderList())
        {
            orders.AddIfNotNull(await CreateObservableOrder(productId, amount).ConfigureAwait(false));
        }

        return orders;
    }

    private async Task<ObservableOrder?> CreateObservableOrder(string productId, int amount)
    {
        var product = await _productRepository.GetAsync(productId).ConfigureAwait(false);
        if (product is null)
            return null;

        return new ObservableOrder
        {
            ObservableProduct = product,
            Amount = amount
        };
    }
    
    public Task<List<PickupPoint>> GetPickupPoints()
    {
        return _pickupPointRepository.GetAllAsync();
    }

    public void AddProduct(string productId)
    {
        _orderService.AddProductToOrder(productId);
    }

    public void RemoveProduct(string productId)
    {
        _orderService.RemoveProductFromOrder(productId);
    }

    public Task SaveOrder(Domain.Models.Order order)
    {
        return _orderService.SaveOrder(order);
    }

    public int GetNextOrderId()
    {
        return _orderService.GetLastOrderId().Result + 1;
    }
}