using DemoExam.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoExam.Database;

public partial class TradeContext : ISupplierRepository
{
    async Task<IEnumerable<Supplier>> ISupplierRepository.GetAll()
    {
        return await Suppliers.ToListAsync().ConfigureAwait(false);
    }
}