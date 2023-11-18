using DemoExam.Domain.Models;

namespace DemoExam.Domain.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();

    Task<Product?> GetAsync(string articleNumber);

    Task AddAsync(Product product);

    Task DeleteAsync(string article);

    Task UpdateAsync(Product product);

    Task<int> Count();

    Task<bool> Contains(string articleNumber);
    Task<IEnumerable<Product>> Find(string searchString, string category);
    Task<IEnumerable<Product>> Find(string searchString);
}