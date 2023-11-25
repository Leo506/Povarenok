using DemoExam.Blazor.Services.LocalStorage;
using DemoExam.Blazor.Shared.Dto.Responses;

namespace DemoExam.Blazor.Services.Basket;

public class BasketService : IBasketService
{
    private const string BasketKey = "Basket";
    private readonly ILocalStorageService _localStorageService;
    private Dictionary<string, int> _products = new();
    public event Action? BasketContentChanged;
    public int TotalProductsCount => _products.Sum(x => x.Value);

    public BasketService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task Initialize()
    {
        _products = await _localStorageService.GetAsync<Dictionary<string, int>>(BasketKey) ?? new();
    }
    
    public async Task AddProduct(Product product)
    {
        if (_products.ContainsKey(product.ArticleNumber))
            _products[product.ArticleNumber]++;
        else
            _products[product.ArticleNumber] = 1;
        await _localStorageService.SetAsync(BasketKey, _products);
        BasketContentChanged?.Invoke();
    }

    public async Task RemoveProduct(Product product, bool all = false)
    {
        if (all)
        {
            _products.Remove(product.ArticleNumber);
            await _localStorageService.SetAsync(BasketKey, _products);
            BasketContentChanged?.Invoke();;
            return;
        }
        _products[product.ArticleNumber]--;
        if (_products[product.ArticleNumber] <= 0)
            _products.Remove(product.ArticleNumber);
        await _localStorageService.SetAsync(BasketKey, _products);
        BasketContentChanged?.Invoke();
    }

    public int GetProductAmount(Product product)
    {
        return _products.TryGetValue(product.ArticleNumber, out var amount) 
            ? amount 
            : 0;
    }

    public Dictionary<string, int> GetAll() => _products;
    public async Task Clear()
    {
        _products.Clear();
        await _localStorageService.SetAsync(BasketKey, _products);
        BasketContentChanged?.Invoke();
    }
}