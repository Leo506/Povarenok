namespace DemoExam.Domain.Models;

public class Order
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public DateTime DeliveryDate { get; set; }

    public int PickupPointId { get; set; }

    public int? GetCode { get; set; }

    public DateTime OrderDate { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

    public virtual PickupPoint OrderPickupPointNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
