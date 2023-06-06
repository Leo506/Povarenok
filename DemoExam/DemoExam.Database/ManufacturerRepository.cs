using DemoExam.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoExam.Database;

public partial class TradeContext : IManufacturerRepository
{
    public async Task<IEnumerable<Manufacturer>> GetAll()
    {
        return await Manufacturers.ToListAsync().ConfigureAwait(false);
    }
}