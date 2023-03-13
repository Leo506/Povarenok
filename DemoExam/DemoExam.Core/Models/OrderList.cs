namespace DemoExam.Core.Models;

public class OrderList
{
    public int OrderId { get; set; }

    public string ProductId { get; set; } = null!;

    public int Amount { get; set; }

    public int Id { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}