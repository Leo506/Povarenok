using System.ComponentModel.DataAnnotations;

namespace DemoExam.Core.Models;

public partial class Product
{
    [Required] public string ProductArticleNumber { get; set; } = null!;

    [Required] public string ProductName { get; set; } = null!;

    [Required] public string ProductDescription { get; set; } = null!;

    [Required] public string ProductCategory { get; set; } = null!;

    public byte[]? ProductPhoto { get; set; }

    [Required] public string ProductManufacturer { get; set; } = null!;

    [Required]
    [Range(0.0, double.MaxValue)]
    public decimal ProductCost { get; set; }

    public byte? ProductDiscountAmount { get; set; }

    [Required] [Range(0, int.MaxValue)] public int ProductQuantityInStock { get; set; }

    [Required] [Range(0, byte.MaxValue)] public byte CurrentDiscount { get; set; }

    [Required] public string Supplier { get; set; } = null!;

    public virtual ICollection<OrderList> OrderLists { get; } = new List<OrderList>();
}