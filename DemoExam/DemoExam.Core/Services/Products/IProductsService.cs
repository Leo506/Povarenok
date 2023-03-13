using DemoExam.Core.Models;
using DemoExam.Core.ObservableObjects;

namespace DemoExam.Core.Services.Products;

public interface IProductsService
{
    IEnumerable<ObservableProduct> GetAll();

    IEnumerable<ObservableProduct> GetWhere(Func<Product, bool> predicate);

    int Count();

    void DeleteProduct(ObservableProduct observableProduct);
}