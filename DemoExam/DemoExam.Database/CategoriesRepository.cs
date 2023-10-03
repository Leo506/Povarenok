using DemoExam.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoExam.Database;

public partial class TradeContext : ICategoriesRepository
{
    async Task<IEnumerable<string>> ICategoriesRepository.GetAll()
    {
        return await Products.Select(x => x.ProductCategory).Distinct().ToListAsync();
    }
}