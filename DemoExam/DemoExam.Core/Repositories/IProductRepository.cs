using DemoExam.Core.Models;

namespace DemoExam.Core.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();

    Task<Product?> GetAsync(string articleNumber);

    Task AddAsync(Product product);

    Task DeleteAsync(Product product);

    Task UpdateAsync(Product product);

    Task<int> Count();

    Task<bool> Contains(string articleNumber);
}