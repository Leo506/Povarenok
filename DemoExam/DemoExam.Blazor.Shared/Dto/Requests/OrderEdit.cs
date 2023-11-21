namespace DemoExam.Blazor.Shared.Dto.Requests;

public class OrderEdit
{
    public int? PickupPointId { get; set; }

    public List<Tuple<string, int>> ItemsToDelete { get; set; } = new();
}