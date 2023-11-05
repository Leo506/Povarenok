using DemoExam.Blazor.Shared;

namespace DemoExam.Blazor.Services.Basket;

public class BasketService : IBasketService
{
    private readonly Dictionary<ProductDto, int> _products = new();

    public event Action? BasketContentChanged;
    public int TotalProductsCount => _products.Sum(x => x.Value);
    
    public void AddProduct(ProductDto product)
    {
        if (_products.ContainsKey(product))
            _products[product]++;
        else
            _products[product] = 1;
        BasketContentChanged?.Invoke();
    }

    public void RemoveProduct(ProductDto product, bool all = false)
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

    public int GetProductAmount(ProductDto productDto)
    {
        return _products.TryGetValue(productDto, out var amount) 
            ? amount 
            : 0;
    }

    public Dictionary<ProductDto, int> GetAll() => _products;
}