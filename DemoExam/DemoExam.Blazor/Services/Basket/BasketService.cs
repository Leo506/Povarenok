using DemoExam.Blazor.Shared.Dto.Responses;

namespace DemoExam.Blazor.Services.Basket;

public class BasketService : IBasketService
{
    private readonly Dictionary<Product, int> _products = new();

    public event Action? BasketContentChanged;
    public int TotalProductsCount => _products.Sum(x => x.Value);
    
    public void AddProduct(Product product)
    {
        if (_products.ContainsKey(product))
            _products[product]++;
        else
            _products[product] = 1;
        BasketContentChanged?.Invoke();
    }

    public void RemoveProduct(Product product, bool all = false)
    {
        if (all)
        {
            _products.Remove(product);
            BasketContentChanged?.Invoke();;
            return;
        }
        _products[product]--;
        if (_products[product] <= 0)
            _products.Remove(product);
        BasketContentChanged?.Invoke();
    }

    public int GetProductAmount(Product product)
    {
        return _products.TryGetValue(product, out var amount) 
            ? amount 
            : 0;
    }

    public Dictionary<Product, int> GetAll() => _products;
    public void Clear()
    {
        _products.Clear();
        BasketContentChanged?.Invoke();
    }
}