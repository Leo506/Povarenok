namespace DemoExam.Domain.Models;

public partial class Product
{
    public string ProductArticleNumber { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string ProductDescription { get; set; } = null!;

    public string ProductCategory { get; set; } = null!;

    public byte[]? ProductPhoto { get; set; }

    public decimal ProductCost { get; set; }

    public byte? MaxDiscount { get; set; }
    
    public int ProductQuantityInStock { get; set; }
    
    public byte CurrentDiscount { get; set; }
    
    public int ManufacturerId { get; set; }
    
    public int SupplierId { get; set; }

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual ICollection<OrderList> OrderLists { get; } = new List<OrderList>();

    public virtual Supplier Supplier { get; set; } = null!;

    public Product()
    {
    }
}
