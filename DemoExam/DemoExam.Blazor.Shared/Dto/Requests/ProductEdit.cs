namespace DemoExam.Blazor.Shared.Dto.Requests;

public class ProductEdit
{
    public string? Name { get; set; } = default!;

    public string? Description { get; set; } = null!;

    public string? Category { get; set; } = null!;

    public string? Photo { get; set; }

    public decimal? Price { get; set; }
    
    public int? QuantityInStock { get; set; }
    
    public byte? Discount { get; set; }

    public string? ManufacturerName { get; set; } = default!;

    public string? SupplierName { get; set; } = default!;
    
    
}