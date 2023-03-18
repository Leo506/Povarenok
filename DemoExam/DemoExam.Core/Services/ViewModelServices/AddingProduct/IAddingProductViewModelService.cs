using DemoExam.Core.ObservableObjects;

namespace DemoExam.Core.Services.ViewModelServices.AddingProduct;

public interface IAddingProductViewModelService
{
    Task<bool> IsValidProduct(ObservableProduct observableProduct);
    Task AddProductAsync(ObservableProduct observableProduct);
}