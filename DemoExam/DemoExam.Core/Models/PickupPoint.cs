using System;
using System.Collections.Generic;

namespace DemoExam.Core.Models;

public partial class PickupPoint
{
    public int PointId { get; set; }

    public string PostIndex { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
