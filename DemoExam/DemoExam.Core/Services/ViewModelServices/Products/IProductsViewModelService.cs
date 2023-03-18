using DemoExam.Core.ObservableObjects;

namespace DemoExam.Core.Services.ViewModelServices.Products;

public interface IProductsViewModelService
{
    Task<int> GetProductsCount();

    Task DeleteProduct(ObservableProduct observableProduct);

    Task AddProductToOrder(ObservableProduct observableProduct);
    
    bool CanOpenOrder();
    
    Task<IEnumerable<ObservableProduct>> GetAllProducts();
}