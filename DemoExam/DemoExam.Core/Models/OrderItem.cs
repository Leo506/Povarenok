using DemoExam.Core.NotifyObjects;

namespace DemoExam.Core.Models;

public class OrderItem
{
    public ProductNotifyObject Product { get; set; } = default!;

    public int Amount { get; set; }
}