namespace DemoExam.Core.Models;

public class OrderItem
{
    public Product Product { get; set; } = default!;

    public int Amount { get; set; }
}