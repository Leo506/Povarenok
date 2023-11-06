namespace DemoExam.Blazor.Shared.Dto.Responses;

public class Order
{
    public int Id { get; set; }

    public string Status { get; set; } = default!;

    public DateOnly DeliveryDate { get; set; }
    
    public DateOnly OrderDate { get; set; }

    public string PickupPoint { get; set; } = default!;

    public int GetCode { get; set; }

    public List<OrderItem> OrderItems { get; set; } = default!;
}