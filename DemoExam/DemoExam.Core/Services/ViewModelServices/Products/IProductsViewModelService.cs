using DemoExam.Core.ObservableObjects;
using DemoExam.Core.ViewModels;

namespace DemoExam.Core.Services.ViewModelServices.Products;

public interface IProductsViewModelService
{
    IEnumerable<ObservableProduct> SelectProducts(string? search = null, SortOrder sortOrder = SortOrder.None,
        Func<double, bool>? discountSelectorPredicate = null);

    IEnumerable<ObservableProduct> GetAllProducts();

    int GetProductsCount();

    void DeleteProduct(ObservableProduct observableProduct);

    void AddProductToOrder(ObservableProduct observableProduct);
    bool CanOpenOrder();
}