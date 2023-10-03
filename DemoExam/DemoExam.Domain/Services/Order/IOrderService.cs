using System.Collections.Immutable;

namespace DemoExam.Domain.Services.Order;

public interface IOrderService
{
    void CreateNewOrder();

    void AddProductToOrder(string productArticleNumber);

    Task SaveOrder(Domain.Models.Order order);

    bool HasProductsInOrder();

    ImmutableDictionary<string, int> GetOrderList();
    
    void RemoveProductFromOrder(string productId);
    Task<int> GetLastOrderId();
}