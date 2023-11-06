using DemoExam.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoExam.Infrastructure;

internal class ManufacturerRepository : IManufacturerRepository
{
    private readonly TradeContext _tradeContext;

    public ManufacturerRepository(TradeContext tradeContext)
    {
        _tradeContext = tradeContext;
    }

    public async Task<IEnumerable<Manufacturer>> GetAll()
    {
        return await _tradeContext.Manufacturers.ToListAsync().ConfigureAwait(false);
    }
}