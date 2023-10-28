namespace DemoExam.Blazor.Shared;

public class OrderShortDto
{
    public int Id { get; set; }

    public string Status { get; set; } = default!;

    public DateOnly DeliveryDate { get; set; } = default!;

    public string PickupPoint { get; set; } = default!;

    public int GetCode { get; set; }
}