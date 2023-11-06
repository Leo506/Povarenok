namespace DemoExam.Domain.Models;

public partial class Product
{
    public string ArticleNumber { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Category { get; set; } = null!;

    public byte[]? Photo { get; set; }

    public decimal Price { get; set; }
    
    public int QuantityInStock { get; set; }

    public byte Discount { get; set; }

    public int ManufacturerId { get; set; }

    public int SupplierId { get; set; }

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

    public virtual Supplier Supplier { get; set; } = null!;
}
