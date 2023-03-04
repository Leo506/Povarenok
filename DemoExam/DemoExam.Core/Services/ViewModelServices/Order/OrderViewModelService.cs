using DemoExam.Core.Contexts;
using DemoExam.Core.Models;
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

    public IEnumerable<OrderItem> GetProductInOrder()
    {
        var items = new List<OrderItem>();
        foreach (var (productId, amount) in _orderService.GetOrderList())
        {
            var product = _tradeContext.Products.First(x => x.ProductArticleNumber == productId);
            items.Add(new OrderItem
            {
                Product = product,
                Amount = amount
            });
        }

        return items;
    }

    public IEnumerable<PickupPoint> GetPickupPoints()
    {
        return _tradeContext.PickupPoints.ToList();
    }
}