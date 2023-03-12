using DemoExam.Core.NotifyObjects;
using DemoExam.Domain.Entities;

namespace DemoExam.Core.Services.Products;

public interface IProductsService
{
    IEnumerable<ProductNotifyObject> GetAll();

    IEnumerable<ProductNotifyObject> GetWhere(Func<Product, bool> predicate);

    int Count();

    void DeleteProduct(ProductNotifyObject product);
}