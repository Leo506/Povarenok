namespace DemoExam.Blazor.Shared;

public class OrderDto
{
    public int Id { get; set; }

    public string Status { get; set; } = default!;

    public DateOnly DeliveryDate { get; set; }
    
    public DateOnly OrderDate { get; set; }

    public string PickupPoint { get; set; } = default!;

    public int GetCode { get; set; }

    public List<OrderItemDto> OrderItems { get; set; } = default!;
}