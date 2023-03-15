using DemoExam.Core.Contexts;
using DemoExam.Core.Models;
using DemoExam.Core.ObservableObjects;
using DemoExam.Core.Services.Order;

namespace DemoExam.Core.Services.ViewModelServices.Order;

public class OrderViewModelService : IOrderViewModelService
{
    private readonly IOrderService _orderService;
    private readonly TradeContext _tradeContext;

    public OrderViewModelService(IOrderService orderService, TradeContext tradeContext)
    {
        _orderService = orderService;
        _tradeContext = tradeContext;
    }

    public IEnumerable<ObservableOrder> GetProductInOrder()
    {
        var items = new List<ObservableOrder>();
        foreach (var (productId, amount) in _orderService.GetOrderList())
        {
            var product = _tradeContext.Products.First(x => x.ProductArticleNumber == productId);
            items.Add(new ObservableOrder
            {
                ObservableProduct = new ObservableProduct(product),
                Amount = amount
            });
        }

        return items;
    }

    public IEnumerable<PickupPoint> GetPickupPoints()
    {
        return _tradeContext.PickupPoints.ToList();
    }

    public void AddProduct(string productId)
    {
        _orderService.AddProductToOrder(productId);
    }

    public void RemoveProduct(string productId)
    {
        _orderService.RemoveProductFromOrder(productId);
    }

    public Task SaveOrder(Models.Order order)
    {
        return _orderService.SaveOrder(order);
    }
}