namespace DemoExam.Blazor.Shared;

public class OrderItemDto
{
    public string Article { get; set; } = default!;

    public string Title { get; set; } = default!;

    public int Amount { get; set; }
    
    public decimal TotalCost { get; set; }
}