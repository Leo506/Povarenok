using DemoExam.Domain.Services.Orders;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

public class OrdersViewModel : MvxViewModel
{
    private readonly IOrdersService _ordersService;
    private Order? _selectedOrder;

    public MvxObservableCollection<Order> Orders { get; set; } = new();

    public Order? SelectedOrder
    {
        get => _selectedOrder;
        set
        {
            if (value is null)
                return;
            
            ProductsInOrder = value.OrderLists.ToDictionary(x => x.Product, x => x.Amount);
            SetProperty(ref _selectedOrder, value);
            RaisePropertyChanged(nameof(ProductsInOrder));
        }
    }
    
    public List<string> OrderStatuses => new() { OrderStatusConstants.NewOrder, OrderStatusConstants.Completed };
    public Dictionary<Product, int> ProductsInOrder { get; private set; }

    public OrdersViewModel(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }
    
    public override async Task Initialize()
    {
        Orders = new(await _ordersService.GetAllOrders());
        await RaisePropertyChanged(nameof(Orders));
    }

    public override async void ViewDisappeared()
    {
        await UpdateSelectedOrder().ConfigureAwait(false);
    }

    private async Task UpdateSelectedOrder()
    {
        if (SelectedOrder is null)
            return;
        await _ordersService.UpdateOrder(SelectedOrder).ConfigureAwait(false);
    }
}