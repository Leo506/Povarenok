using DemoExam.Core.Contexts;
using DemoExam.Core.Models;
using Microsoft.IdentityModel.Tokens;

namespace DemoExam.Core.Services.Order;

public class OrderService : IOrderService
{
    private Dictionary<string, int>? _productsInOrder;
    private readonly TradeContext _tradeContext;

    public OrderService(TradeContext tradeContext)
    {
        _tradeContext = tradeContext;
    }

    public void CreateNewOrder()
    {
        _productsInOrder = new Dictionary<string, int>();
    }

    public void AddProductToOrder(string productArticleNumber)
    {
        if (_productsInOrder is null)
            throw new InvalidOperationException("Create order before adding product");

        if (_productsInOrder.ContainsKey(productArticleNumber))
            _productsInOrder[productArticleNumber]++;
        else
            _productsInOrder[productArticleNumber] = 1;
    }

    public async Task SaveOrder(Models.Order order)
    {
        if (_productsInOrder is null || _productsInOrder.Count == 0)
            throw new InvalidOperationException("Add products in order before saving");
        
        var addedOrderEntity =  await _tradeContext.Orders.AddAsync(order);
        foreach (var (product, amount) in _productsInOrder)
        {
            await _tradeContext.OrderLists.AddAsync(new OrderList()
            {
                OrderId = addedOrderEntity.Entity.OrderId,
                ProductId = product,
                Amount = amount
            });
        }

        await _tradeContext.SaveChangesAsync();
    }
}