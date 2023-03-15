using System.Collections.Immutable;

namespace DemoExam.Core.Services.Order;

public interface IOrderService
{
    void CreateNewOrder();

    void AddProductToOrder(string productArticleNumber);

    Task SaveOrder(Models.Order order);

    bool HasProductsInOrder();

    ImmutableDictionary<string, int> GetOrderList();
    
    void RemoveProductFromOrder(string productId);
    Task<int> GetLastOrderId();
}