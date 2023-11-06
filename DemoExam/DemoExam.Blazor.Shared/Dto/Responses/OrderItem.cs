namespace DemoExam.Blazor.Shared.Dto.Responses;

public class OrderItem
{
    public string Article { get; set; } = default!;

    public string Title { get; set; } = default!;

    public int Amount { get; set; }
    
    public decimal TotalCost { get; set; }
}