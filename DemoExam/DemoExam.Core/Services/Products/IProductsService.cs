using DemoExam.Core.Models;
using DemoExam.Core.NotifyObjects;

namespace DemoExam.Core.Services.Products;

public interface IProductsService
{
    IEnumerable<ProductNotifyObject> GetAll();

    IEnumerable<ProductNotifyObject> GetWhere(Func<Product, bool> predicate);

    int Count();
    
    List<ProductOperation> GetAvailableProductsOperationsForUser(User user);
}