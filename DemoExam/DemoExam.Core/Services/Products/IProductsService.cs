using DemoExam.Core.Models;

namespace DemoExam.Core.Services.Products;

public interface IProductsService
{
    IEnumerable<Product> GetAll();

    Task<IEnumerable<Product>> GetAllAsync();
    
    IEnumerable<Product> GetWhere(Func<Product, bool> predicate);

    int Count();
}