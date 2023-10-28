namespace DemoExam.Domain.Models;

public partial class PickupPoint
{
    public int Id { get; set; }

    public string PostIndex { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
