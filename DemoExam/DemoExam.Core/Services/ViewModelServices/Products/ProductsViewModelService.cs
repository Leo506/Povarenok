using DemoExam.Core.ObservableObjects;
using DemoExam.Core.Services.Order;
using DemoExam.Core.Services.Products;
using DemoExam.Core.ViewModels;

namespace DemoExam.Core.Services.ViewModelServices.Products;

public class ProductsViewModelService : IProductsViewModelService
{
    private readonly IOrderService _orderService;
    private readonly IProductsService _productsService;

    public ProductsViewModelService(IProductsService productsService, IOrderService orderService)
    {
        _productsService = productsService;
        _orderService = orderService;
    }

    public IEnumerable<ObservableProduct> SelectProducts(string? search = null, SortOrder sortOrder = SortOrder.None,
        Func<double, bool>? discountSelectorPredicate = null)
    {
        IEnumerable<ObservableProduct> products;
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

    public IEnumerable<ObservableProduct> GetAllProducts()
    {
        return _productsService.GetAll();
    }

    public int GetProductsCount()
    {
        return _productsService.Count();
    }

    public void DeleteProduct(ObservableProduct observableProduct)
    {
        _productsService.DeleteProduct(observableProduct);
    }

    public void AddProductToOrder(ObservableProduct observableProduct)
    {
        _orderService.AddProductToOrder(observableProduct.ProductArticleNumber);
    }

    public bool CanOpenOrder()
    {
        return _orderService.HasProductsInOrder();
    }
}