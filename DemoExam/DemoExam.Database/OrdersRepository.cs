using DemoExam.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoExam.Database;

public partial class TradeContext : IOrdersRepository
{
    async Task<IEnumerable<Order>> IOrdersRepository.GetAllAsync()
    {
        var orders = await Orders
            .Include(x => x.OrderLists)
                .ThenInclude(x => x.Product)
            .Include(x => x.OrderPickupPointNavigation)
            .ToListAsync();

        return orders;
    }
}