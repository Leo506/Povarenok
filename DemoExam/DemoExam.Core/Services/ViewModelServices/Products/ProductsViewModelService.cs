using DemoExam.Core.NotifyObjects;
using DemoExam.Core.Services.Order;
using DemoExam.Core.Services.Products;
using DemoExam.Core.ViewModels;

namespace DemoExam.Core.Services.ViewModelServices.Products;

public class ProductsViewModelService : IProductsViewModelService
{
    private readonly IProductsService _productsService;
    private readonly IOrderService _orderService;

    public ProductsViewModelService(IProductsService productsService, IOrderService orderService)
    {
        _productsService = productsService;
        _orderService = orderService;
    }

    public IEnumerable<ProductNotifyObject> SelectProducts(string? search = null, SortOrder sortOrder = SortOrder.None,
        Func<double, bool>? discountSelectorPredicate = null)
    {
        IEnumerable<ProductNotifyObject> products;
        if (string.IsNullOrEmpty(search))
            products = _productsService.GetAll();
        else
            products = _productsService.GetWhere(p =>
                p.ProductName.ToLowerInvariant().Contains(search.ToLowerInvariant()));

        if (discountSelectorPredicate is not null)
            products = products.Where(x => discountSelectorPredicate(x.CurrentDiscount));

        products = sortOrder switch
        {
            SortOrder.Asc => products.OrderBy(x => x.ProductCost),
            SortOrder.Desc => products.OrderByDescending(x => x.ProductCost),
            _ => products
        };

        return products;
    }

    public IEnumerable<ProductNotifyObject> GetAllProducts() => _productsService.GetAll();

    public int GetProductsCount() => _productsService.Count();

    public void DeleteProduct(ProductNotifyObject product) => _productsService.DeleteProduct(product);
    
    public void AddProductToOrder(ProductNotifyObject product) => _orderService.AddProductToOrder(product.ProductArticleNumber);
    public bool CanOpenOrder()
    {
        return _orderService.HasProductsInOrder();
    }
}