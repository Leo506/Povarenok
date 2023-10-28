namespace DemoExam.Domain.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string SupplierName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
