using DemoExam.Core.Models;
using DemoExam.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoExam.Database;

public partial class TradeContext : IProductRepository
{
    public Task<List<Product>> GetAllAsync()
    {
        return Products.ToListAsync();
    }

    public Task<Product?> GetAsync(string articleNumber)
    {
        return Products.FirstOrDefaultAsync(x => x.ProductArticleNumber == articleNumber);
    }

    public async Task AddAsync(Product product)
    {
        await Products.AddAsync(product);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        Products.Remove(product);
        await SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        Products.Update(product);
        await SaveChangesAsync();
    }

    public Task<int> Count()
    {
        return Products.CountAsync();
    }

    public Task<bool> Contains(string articleNumber) => 
        Products.AnyAsync(x => x.ProductArticleNumber == articleNumber);
}