using System;
using System.Collections.Generic;

namespace DemoExam.Core.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string OrderStatus { get; set; } = null!;

    public DateTime OrderDeliveryDate { get; set; }

    public int OrderPickupPoint { get; set; }

    public string? ClientName { get; set; }

    public int? GetCode { get; set; }

    public DateTime OrderData { get; set; }

    public virtual ICollection<OrderList> OrderLists { get; } = new List<OrderList>();

    public virtual PickupPoint OrderPickupPointNavigation { get; set; } = null!;
}
