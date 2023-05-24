using DemoExam.Core.ObservableObjects;
using DemoExam.Core.Repositories;
using DemoExam.Core.Services.Order;

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
        var items = new List<ObservableOrder>();
        foreach (var (productId, amount) in _orderService.GetOrderList())
        {
            var product = await _productRepository.GetAsync(productId).ConfigureAwait(false);
            items.Add(new ObservableOrder
            {
                ObservableProduct = product!, // TODO red flag!
                Amount = amount
            });
        }

        return items;
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