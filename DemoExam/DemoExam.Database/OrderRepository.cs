using DemoExam.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoExam.Database;

public partial class TradeContext : IOrderRepository
{
    public async Task<Order> CreateOrderAsync(Order order)
    {
        order.Id = 0;
        var orderEntity = await Orders.AddAsync(order).ConfigureAwait(false);
        await SaveChangesAsync().ConfigureAwait(false);

        return orderEntity.Entity;
    }

    public async Task AddProductPositionToOrder(int orderId, string productId, int amount)
    {
        await OrderItems.AddAsync(new OrderItem()
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
            .OrderBy(x => x.Id)
            .Select(x => x.Id)
            .LastAsync();
    }

    public Task UpdateOrder(Order order)
    {
        Orders.Update(order);
        return SaveChangesAsync();
    }
}