using DemoExam.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoExam.Infrastructure;

internal class CategoriesRepository : ICategoriesRepository
{
    private readonly TradeContext _tradeContext;

    public CategoriesRepository(TradeContext tradeContext)
    {
        _tradeContext = tradeContext;
    }

    async Task<IEnumerable<string>> ICategoriesRepository.GetAll()
    {
        return await _tradeContext.Products.Select(x => x.Category).Distinct().ToListAsync();
    }
}