using System.Collections.Immutable;

namespace DemoExam.Core.Services.Order;

public interface IOrderService
{
    public void CreateNewOrder();

    public void AddProductToOrder(string productArticleNumber);

    public Task SaveOrder(Models.Order order);

    bool HasProductsInOrder();

    ImmutableDictionary<string, int> GetOrderList();
}