using DemoExam.Blazor.Shared.Attributes;
using DemoExam.Blazor.Shared.Dto.Requests;
using DemoExam.Blazor.Shared.Dto.Responses;

namespace DemoExam.Blazor.ViewModels;

public class OrderEditViewModel
{
    public int Id { get; private set; }

    [OrderStatus]
    public string Status { get; set; } = default!;
    
    public DateOnly DeliveryDate { get; set; }
    
    public DateOnly OrderDate { get; private set; }
    
    public int PickupPointId { get; set; }

    public int GetCode { get; set; }

    public List<OrderItem> OrderItems { get; private set; }

    private Dictionary<string, int> _itemsToDelete = new();

    private Dictionary<string, int> _originalOrderItemsCount = new();

    public OrderEditViewModel(Order order)
    {
        Id = order.Id;
        Status = order.Status;
        DeliveryDate = order.DeliveryDate;
        OrderDate = order.OrderDate;
        PickupPointId = order.PickupPointId;
        GetCode = order.GetCode;
        OrderItems = order.OrderItems;

        _originalOrderItemsCount = OrderItems.ToDictionary(x => x.Article, x => x.Amount);
    }

    public void DeleteItem(string article)
    {
        var orderItem = OrderItems.First(x => x.Article == article);
        orderItem.Amount--;
        if (orderItem.Amount <= 0)
            OrderItems.Remove(orderItem);
        if (_itemsToDelete.ContainsKey(article))
            _itemsToDelete[article]++;
        else
            _itemsToDelete.Add(article, 1);
    }

    public void AddItem(string article)
    {
        var orderItem = OrderItems.First(x => x.Article == article);
        if (orderItem.Amount + 1 <= _originalOrderItemsCount[article])
        {
            orderItem.Amount++;
            _itemsToDelete[article]--;
            if (_itemsToDelete[article] <= 0)
                _itemsToDelete.Remove(article);
        }
    }

    public bool CanAddItem(string article)
    {
        var orderItem = OrderItems.First(x => x.Article == article);
        return orderItem.Amount < _originalOrderItemsCount[article];
    }

    public OrderEdit ToDto()
    {
        return new OrderEdit()
        {
            PickupPointId = PickupPointId,
            ItemsToDelete = _itemsToDelete,
            Status = Status,
            DeliveryDate = DeliveryDate.ToDateTime(TimeOnly.MinValue)
        };
    }
}