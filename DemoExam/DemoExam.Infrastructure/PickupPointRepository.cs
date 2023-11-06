using DemoExam.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoExam.Infrastructure;

internal class PickupPointRepository : IPickupPointRepository
{
    private readonly TradeContext _tradeContext;

    public PickupPointRepository(TradeContext tradeContext)
    {
        _tradeContext = tradeContext;
    }

    Task<List<PickupPoint>> IPickupPointRepository.GetAllAsync() => _tradeContext.PickupPoints.ToListAsync();
}