using System.Collections.Immutable;
using DemoExam.Core.Repositories;

namespace DemoExam.Core.Services.Order;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private Dictionary<string, int>? _productsInOrder;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public void CreateNewOrder()
    {
        _productsInOrder = new Dictionary<string, int>();
    }

    public void AddProductToOrder(string productArticleNumber)
    {
        if (_productsInOrder is null)
            throw new InvalidOperationException("Create order before adding product");

        if (_productsInOrder.ContainsKey(productArticleNumber))
            _productsInOrder[productArticleNumber]++;
        else
            _productsInOrder[productArticleNumber] = 1;
    }

    public async Task SaveOrder(Models.Order order)
    {
        if (_productsInOrder is null || _productsInOrder.Count == 0)
            throw new InvalidOperationException("Add products in order before saving");

        var orderEntity = await _repository.CreateOrderAsync(order).ConfigureAwait(false);

        foreach (var (product, amount) in _productsInOrder)
            await _repository.AddProductPositionToOrder(orderEntity.OrderId, product, amount).ConfigureAwait(false);

        CreateNewOrder();
    }

    public bool HasProductsInOrder()
    {
        return _productsInOrder is not null && _productsInOrder.Count > 0;
    }

    public ImmutableDictionary<string, int> GetOrderList()
    {
        if (_productsInOrder is null)
            throw new InvalidOperationException("Create order first");
        return _productsInOrder.ToImmutableDictionary();
    }

    public void RemoveProductFromOrder(string productId)
    {
        if (_productsInOrder is null)
            throw new InvalidOperationException("Create order first");

        if (_productsInOrder.ContainsKey(productId) is false)
            throw new InvalidOperationException($"There is no product with id {productId} in order");

        _productsInOrder[productId]--;
        if (_productsInOrder[productId] == 0)
            _productsInOrder.Remove(productId);
    }
}