namespace DemoExam.Domain.Models;

public class Supplier
{
    public int Id { get; set; }

    public string SupplierName { get; set; } = null!;

    public virtual ICollection<Domain.Models.Product> Products { get; } = new List<Domain.Models.Product>();
}
