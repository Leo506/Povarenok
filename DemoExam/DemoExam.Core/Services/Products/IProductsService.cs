using DemoExam.Core.ObservableObjects;

namespace DemoExam.Core.Services.Products;

public interface IProductsService
{
    Task<IEnumerable<ObservableProduct>> GetAll();
    
    Task<int> Count();

    Task DeleteProduct(ObservableProduct observableProduct);

    Task AddProduct(ObservableProduct observableProduct);

    Task UpdateProduct(ObservableProduct observableProduct);
}