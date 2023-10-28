using System.ComponentModel.DataAnnotations.Schema;

namespace DemoExam.Domain.Models;

public partial class PickupPoint
{
    [NotMapped] public string AddressString => $"{PostIndex} {City} {Street}";
}