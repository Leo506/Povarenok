using DemoExam.Core.Models;
using MvvmCross.ViewModels;

namespace DemoExam.Core.Services.Products;

public interface IProductsService
{
    IEnumerable<Product> GetAll();

    Task<IEnumerable<Product>> GetAllAsync();
    
    IEnumerable<Product> GetWhere(Func<Product, bool> predicate);

    int Count();
    List<ProductOperation> GetAvailableProductsOperationsForUser(User user);
}