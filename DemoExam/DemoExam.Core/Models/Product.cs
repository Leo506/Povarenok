using System;
using System.Collections.Generic;

namespace DemoExam.Core.Models;

public partial class Product
{
    public string ProductArticleNumber { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string ProductDescription { get; set; } = null!;

    public string ProductCategory { get; set; } = null!;

    public byte[]? ProductPhoto { get; set; }

    public string ProductManufacturer { get; set; } = null!;

    public decimal ProductCost { get; set; }

    public byte? ProductDiscountAmount { get; set; }

    public int ProductQuantityInStock { get; set; }

    public byte CurrentDiscount { get; set; }

    public string Supplier { get; set; } = null!;

    public virtual ICollection<OrderList> OrderLists { get; } = new List<OrderList>();
}
