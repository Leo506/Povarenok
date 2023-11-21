namespace DemoExam.Blazor.Shared.Dto.Requests;

public class OrderEdit
{
    public int? PickupPointId { get; set; }

    public Dictionary<string, int> ItemsToDelete { get; set; } = new();
}