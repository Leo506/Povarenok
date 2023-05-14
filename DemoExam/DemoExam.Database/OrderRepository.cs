using DemoExam.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoExam.Database;

public partial class TradeContext : IOrderRepository
{
    public async Task<Order> CreateOrderAsync(Order order)
    {
        order.OrderId = 0;
        var orderEntity = await Orders.AddAsync(order).ConfigureAwait(false);
        await SaveChangesAsync().ConfigureAwait(false);

        return orderEntity.Entity;
    }

    public async Task AddProductPositionToOrder(int orderId, string productId, int amount)
    {
        await OrderLists.AddAsync(new OrderList()
        {
            OrderId = orderId,
            ProductId = productId,
            Amount = amount
        }).ConfigureAwait(false);

        await SaveChangesAsync().ConfigureAwait(false);
    }

    public Task<int> GetLastOrderId()
    {
        return Orders
            .OrderBy(x => x.OrderId)
            .Select(x => x.OrderId)
            .LastAsync();
    }

    public Task UpdateOrder(Order order)
    {
        Orders.Update(order);
        return SaveChangesAsync();
    }
}