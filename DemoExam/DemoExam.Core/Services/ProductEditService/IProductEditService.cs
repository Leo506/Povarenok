using DemoExam.Core.ObservableObjects;

namespace DemoExam.Core.Services.ProductEditService;

public interface IProductEditService
{
    Task SaveProduct(ObservableProduct observableProduct);
}