using System.ComponentModel.DataAnnotations;

namespace DemoExam.Domain.Models;

public partial class Product
{
    [Required] public string ProductArticleNumber { get; set; } = null!;

    [Required] public string ProductName { get; set; } = null!;

    [Required] public string ProductDescription { get; set; } = null!;

    [Required] public string ProductCategory { get; set; } = null!;

    public byte[]? ProductPhoto { get; set; }

    public decimal ProductCost { get; set; }

    public byte? MaxDiscount { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int ProductQuantityInStock { get; set; }

    [Required]
    [Range(0, byte.MaxValue)]
    public byte CurrentDiscount { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int ManufacturerId { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int SupplierId { get; set; }

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual ICollection<OrderList> OrderLists { get; } = new List<OrderList>();

    public virtual Supplier Supplier { get; set; } = null!;
}
