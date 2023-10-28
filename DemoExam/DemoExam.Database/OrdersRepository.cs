using DemoExam.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoExam.Database;

public partial class TradeContext : IOrdersRepository
{
    async Task<IEnumerable<Order>> IOrdersRepository.GetAllAsync()
    {
        var orders = await Orders
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
            .Include(x => x.OrderPickupPointNavigation)
            .ToListAsync();

        return orders;
    }

    public Task<List<Order>> GetOrdersForUser(int userId)
    {
        return Orders
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
            .Include(x => x.OrderPickupPointNavigation)
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public Task<Order?> Get(int orderId)
    {
        return Orders
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.Product)
            .Include(x => x.OrderPickupPointNavigation)
            .FirstOrDefaultAsync(x => x.Id == orderId);
    }
}