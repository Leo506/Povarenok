using DemoExam.Core.Models;
using DemoExam.Core.Services.ViewModelServices.Order;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

public class OrderViewModel : MvxViewModel<User>
{
    private readonly IOrderViewModelService _viewModelService;

    private User _user = default!;

    public OrderViewModel(IOrderViewModelService viewModelService)
    {
        _viewModelService = viewModelService;
        ProductsInOrder = new MvxObservableCollection<OrderItem>(_viewModelService.GetProductInOrder());
        PickupPoints = _viewModelService.GetPickupPoints();
    }

    public Order Order { get; set; }

    public MvxObservableCollection<OrderItem> ProductsInOrder { get; set; }

    public IEnumerable<PickupPoint> PickupPoints { get; set; }

    public override void Prepare(User parameter)
    {
        _user = parameter;
        Order = new Order
        {
            ClientName = _user.FullName,
            OrderData = DateTime.Now,
            OrderDeliveryDate = DateTime.Now.AddDays(3)
        };
    }
}