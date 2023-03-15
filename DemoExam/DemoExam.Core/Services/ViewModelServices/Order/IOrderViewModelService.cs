using DemoExam.Core.Models;
using DemoExam.Core.ObservableObjects;

namespace DemoExam.Core.Services.ViewModelServices.Order;

public interface IOrderViewModelService
{
    IEnumerable<ObservableOrder> GetProductInOrder();
    IEnumerable<PickupPoint> GetPickupPoints();
    void AddProduct(string productId);
    void RemoveProduct(string productId);
    Task SaveOrder(Models.Order order);
}