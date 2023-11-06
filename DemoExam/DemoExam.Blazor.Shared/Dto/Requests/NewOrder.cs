using System.ComponentModel.DataAnnotations;

namespace DemoExam.Blazor.Shared.Dto.Requests;

public class NewOrder
{
    [Required]
    public Dictionary<string, int> Products { get; set; } = default!;

    [Required]
    [Range(1, int.MaxValue)]
    public int PickupPointId { get; set; }
}