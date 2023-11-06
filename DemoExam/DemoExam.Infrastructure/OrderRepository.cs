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

        await _tradeContext.SaveChangesAsync().ConfigureAwait(false);
    }

    public Task<int> GetLastOrderId()
    {
        return _tradeContext.Orders
            .OrderBy(x => x.Id)
            .Select(x => x.Id)
            .LastAsync();
    }
}