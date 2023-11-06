using DemoExam.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoExam.Infrastructure;

internal class OrdersRepository : IOrdersRepository
{
    private readonly TradeContext _tradeContext;

    public OrdersRepository(TradeContext tradeContext)
    {
        _tradeContext = tradeContext;
    }

    async Task<IEnumerable<Order>> IOrdersRepository.GetAllAsync()
    {
        var orders = await _tradeContext.Orders
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
            .Include(x => x.OrderPickupPointNavigation)
            .ToListAsync();

        return orders;
    }

    public Task<List<Order>> GetOrdersForUser(int userId)
    {
        return _tradeContext.Orders
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
            .Include(x => x.OrderPickupPointNavigation)
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public Task<Order?> Get(int orderId)
    {
        return _tradeContext.Orders
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.Product)
            .Include(x => x.OrderPickupPointNavigation)
            .FirstOrDefaultAsync(x => x.Id == orderId);
    }
    
    public Task UpdateOrder(Order order)
    {
        _tradeContext.Orders.Update(order);
        return _tradeContext.SaveChangesAsync();
    }
}