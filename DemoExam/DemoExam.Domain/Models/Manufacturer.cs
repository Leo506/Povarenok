namespace DemoExam.Domain.Models;

public partial class Manufacturer
{
    public int Id { get; set; }

    public string ManufacturerName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
