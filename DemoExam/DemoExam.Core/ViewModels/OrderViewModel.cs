using System.Windows.Input;
using DemoExam.Core.Models;
using DemoExam.Core.ObservableObjects;
using DemoExam.Core.Services.ViewModelServices.Order;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

public class OrderViewModel : MvxViewModel<User>
{
    private readonly IMvxNavigationService _navigationService;

    private readonly IOrderViewModelService _viewModelService;
    private User _user = default!;

    public OrderViewModel(IOrderViewModelService viewModelService, IMvxNavigationService navigationService)
    {
        _viewModelService = viewModelService;
        _navigationService = navigationService;
        ProductsInOrder = new MvxObservableCollection<ObservableOrder>(_viewModelService.GetProductInOrder());
        PickupPoints = _viewModelService.GetPickupPoints();
    }

    public Order Order { get; set; }

    public MvxObservableCollection<ObservableOrder> ProductsInOrder { get; set; }

    public IEnumerable<PickupPoint> PickupPoints { get; set; }

    public decimal OrderSum => ProductsInOrder.Sum(x => x.ObservableProduct.ProductCostWithDiscount * x.Amount);

    public decimal OrderDiscount => ProductsInOrder.Sum(x => x.ObservableProduct.ProductCost * x.Amount) - OrderSum;

    public ICommand AddProductCommand => new MvxCommand<string>(AddProduct);

    public ICommand RemoveProductCommand => new MvxCommand<string>(RemoveProduct);

    public override void Prepare(User parameter)
    {
        _user = parameter;
        Order = new Order
        {
            ClientName = _user.FullName,
            OrderData = DateTime.Now,
            OrderDeliveryDate = DateTime.Now.AddDays(3),
            OrderStatus = OrderStatusConstants.NewOrder
        };
    }

    private void AddProduct(string productId)
    {
        _viewModelService.AddProduct(productId);
        var orderItem = ProductsInOrder.FirstOrDefault(x => x.ObservableProduct.ProductArticleNumber == productId);
        if (orderItem is null) return;
        orderItem.Amount++;
        RaisePropertyChanged(nameof(OrderSum));
        RaisePropertyChanged(nameof(OrderDiscount));
    }

    private void RemoveProduct(string productId)
    {
        _viewModelService.RemoveProduct(productId);
        var orderItem = ProductsInOrder.FirstOrDefault(x => x.ObservableProduct.ProductArticleNumber == productId);
        if (orderItem is null) return;
        orderItem.Amount--;

        if (orderItem.Amount == 0) ProductsInOrder.Remove(orderItem);
        if (ProductsInOrder.Any() is false) _navigationService.Close(this);

        RaisePropertyChanged(nameof(OrderSum));
        RaisePropertyChanged(nameof(OrderDiscount));
    }
}