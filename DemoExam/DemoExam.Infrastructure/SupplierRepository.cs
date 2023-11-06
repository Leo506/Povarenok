using DemoExam.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoExam.Infrastructure;

internal class SupplierRepository : ISupplierRepository
{
    private readonly TradeContext _tradeContext;

    public SupplierRepository(TradeContext tradeContext)
    {
        _tradeContext = tradeContext;
    }

    async Task<IEnumerable<Supplier>> ISupplierRepository.GetAll()
    {
        return await _tradeContext.Suppliers.ToListAsync().ConfigureAwait(false);
    }
}