using DemoExam.Core.Services.Orders;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

public class OrdersViewModel : MvxViewModel
{
    private readonly IOrdersService _ordersService;

    public MvxObservableCollection<Order> Orders { get; set; } = new();
    
    public OrdersViewModel(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }
    
    public override async Task Initialize()
    {
        Orders = new(await _ordersService.GetAllOrders());
        await RaisePropertyChanged(nameof(Orders));
    }
}