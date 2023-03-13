using DemoExam.Core.ObservableObjects;
using DemoExam.Core.ViewModels;

namespace DemoExam.Core.Services.ViewModelServices.Products;

public interface IProductsViewModelService
{
    Task<IEnumerable<ObservableProduct>> SelectProducts(string? search = null, SortOrder sortOrder = SortOrder.None,
        Func<double, bool>? discountSelectorPredicate = null);

    Task<int> GetProductsCount();

    Task DeleteProduct(ObservableProduct observableProduct);

    Task AddProductToOrder(ObservableProduct observableProduct);
    
    bool CanOpenOrder();
}