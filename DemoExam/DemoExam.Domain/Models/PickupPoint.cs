namespace DemoExam.Domain.Models;

public class PickupPoint
{
    public int PointId { get; set; }

    public string PostIndex { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public override string ToString()
    {
        return $"{PostIndex} {City}, {Street}";
    }
}