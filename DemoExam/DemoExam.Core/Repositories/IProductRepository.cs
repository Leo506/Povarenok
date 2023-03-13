using DemoExam.Core.Models;

namespace DemoExam.Core.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();

    Task AddAsync(Product product);

    Task DeleteAsync(Product product);

    Task UpdateAsync(Product product);

    Task<int> Count();
}