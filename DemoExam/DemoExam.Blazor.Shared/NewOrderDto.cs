using System.ComponentModel.DataAnnotations;

namespace DemoExam.Blazor.Shared;

public class NewOrderDto
{
    [Required]
    public Dictionary<string, int> Products { get; set; } = default!;

    [Required]
    [Range(1, int.MaxValue)]
    public int PickupPointId { get; set; }
}