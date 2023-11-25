using DemoExam.Domain.Exceptions;
using DemoExam.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoExam.Infrastructure;

internal class OrderRepository : IOrderRepository
{
    private readonly TradeContext _tradeContext;

    public OrderRepository(TradeContext tradeContext)
    {
        _tradeContext = tradeContext;
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        var orderEntity = await _tradeContext.Orders.AddAsync(order).ConfigureAwait(false);
        await _tradeContext.SaveChangesAsync().ConfigureAwait(false);

        return orderEntity.Entity;
    }

    public async Task AddProductPositionToOrder(int orderId, string productId, int amount)
    {
        await _tradeContext.OrderItems.AddAsync(new OrderItem()
        {
            OrderId = orderId,
            ProductId = productId,
            Amount = amount
        }).ConfigureAwait(false);

        var product = await _tradeContext.Products.FirstAsync(x => x.ArticleNumber == productId);
        product.QuantityInStock -= amount;
        await _tradeContext.SaveChangesAsync().ConfigureAwait(false);
    }

    public Task<int> GetLastOrderId()
    {
        return _tradeContext.Orders
            .OrderBy(x => x.Id)
            .Select(x => x.Id)
            .LastAsync();
    }

    public Task RemoveOrder(Order order)
    {
        _tradeContext.Orders.Remove(order);
        return _tradeContext.SaveChangesAsync();
    }

    public async Task DeleteOrderItems(int orderId, Dictionary<string, int> orderItemsToDelete)
    {
        foreach (var (article, amount) in orderItemsToDelete) 
            await DeleteOrderItem(orderId, article, amount);

        await _tradeContext.SaveChangesAsync();
    }

    private async Task DeleteOrderItem(int orderId, string article, int amount)
    {
        var orderItem = await _tradeContext.OrderItems
            .FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == article);
        if (orderItem is null)
            throw new EntityNotFoundException();

        if (orderItem.Amount <= amount)
            _tradeContext.OrderItems.Remove(orderItem);
        else
        {
            orderItem.Amount -= amount;
            _tradeContext.OrderItems.Update(orderItem);
        }
    }
}