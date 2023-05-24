using DemoExam.Core.ObservableObjects;

namespace DemoExam.Core.Services.ViewModelServices.Order;

public interface IOrderViewModelService
{
    Task<IEnumerable<ObservableOrder>> GetProductsInOrder();
    Task<List<PickupPoint>> GetPickupPoints();
    void AddProduct(string productId);
    void RemoveProduct(string productId);
    Task SaveOrder(Domain.Models.Order order);
    int GetNextOrderId();
}