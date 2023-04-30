namespace DemoExam.Core.Models;

public class Supplier
{
    public int Id { get; set; }

    public string SupplierName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
