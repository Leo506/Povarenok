using System.Diagnostics;
using System.Windows.Input;
using DemoExam.Core.ObservableObjects;
using DemoExam.Core.Services.Alert;
using DemoExam.Core.Services.ViewModelServices.Order;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

public class OrderViewModel : MvxViewModel<User>
{
    private readonly IMvxNavigationService _navigationService;
    private readonly IOrderViewModelService _viewModelService;
    private readonly IAlert _alert;
    private User _user = default!;
    private PickupPoint _selectedPickupPoint;
    
    public Order Order { get; set; }
    
    public MvxObservableCollection<ObservableOrder> ProductsInOrder { get; set; }

    public MvxObservableCollection<PickupPoint> PickupPoints { get; set; }

    public PickupPoint SelectedPickupPoint
    {
        get => _selectedPickupPoint;
        set => SetProperty(ref _selectedPickupPoint, value);
    }

    public decimal OrderSum => ProductsInOrder.Sum(x => x.ObservableProduct.ProductCostWithDiscount * x.Amount);

    public decimal OrderDiscount => ProductsInOrder.Sum(x => x.ObservableProduct.ProductCost * x.Amount) - OrderSum;

    public ICommand AddProductCommand => new MvxCommand<string>(AddProduct);

    public ICommand RemoveProductCommand => new MvxCommand<string>(RemoveProduct);

    public ICommand SaveOrderCommand => new MvxAsyncCommand(SaveOrder);

    public ICommand SaveOrderTicketCommand => new MvxCommand(SaveOrderTicket);

    public OrderViewModel(IOrderViewModelService viewModelService, IMvxNavigationService navigationService, IAlert alert)
    {
        _viewModelService = viewModelService;
        _navigationService = navigationService;
        _alert = alert;
    }

    public override async Task Initialize()
    {
        ProductsInOrder =
            new MvxObservableCollection<ObservableOrder>(await _viewModelService.GetProductsInOrder()
                .ConfigureAwait(false));
        PickupPoints =
            new MvxObservableCollection<PickupPoint>(await _viewModelService.GetPickupPoints().ConfigureAwait(false));
        await RaisePropertyChanged(nameof(PickupPoints)).ConfigureAwait(false);
        await RaisePropertyChanged(nameof(ProductsInOrder)).ConfigureAwait(false);
        SelectedPickupPoint = PickupPoints.First();
    }

    public override void Prepare(User parameter)
    {
        _user = parameter;
        var random = new Random();
        Order = new Order
        {
            OrderId = _viewModelService.GetNextOrderId(),
            ClientName = _user.FullName,
            OrderData = DateTime.Now,
            OrderDeliveryDate = DateTime.Now.AddDays(3),
            OrderStatus = OrderStatusConstants.NewOrder,
            GetCode = random.Next(1, 1000)
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

    private async Task SaveOrder()
    {
        Order.OrderPickupPoint = SelectedPickupPoint.PointId;
        await _viewModelService.SaveOrder(Order).ConfigureAwait(false);
        await _navigationService.Close(this).ConfigureAwait(false);
    }
    
    private void SaveOrderTicket()
    {
        try
        {
            var ticketPath = PdfGenerator.PdfGenerator.CreatePdfForOrder(Order.OrderId, DateTime.Now, OrderSum, OrderDiscount,
                SelectedPickupPoint.ToString(), Order.GetCode ?? 0,
                ProductsInOrder.ToDictionary(x => x.ObservableProduct, x => x.Amount));
            _alert.Alert("Success", "Success");
            // TODO replace by interface call
            new Process()
            {
                StartInfo = new ProcessStartInfo(ticketPath) { UseShellExecute = true }
            }.Start();
        }
        catch (Exception e)
        {
            _alert.Alert("Error", e.Message);
        }
    }
}