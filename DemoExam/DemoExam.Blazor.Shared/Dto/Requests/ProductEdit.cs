using System.ComponentModel.DataAnnotations;

namespace DemoExam.Blazor.Shared.Dto.Requests;

public class ProductEdit
{
    public string? Name { get; set; } = default!;

    public string? Description { get; set; } = null!;

    public string? Category { get; set; } = null!;

    public string? Photo { get; set; }

    [Range(0, double.MaxValue)]
    public decimal? Price { get; set; }
    
    [Range(0, int.MaxValue)]
    public int? QuantityInStock { get; set; }
    
    [Range(0, 100)]
    public byte? Discount { get; set; }

    public string? ManufacturerName { get; set; }

    public string? SupplierName { get; set; }
    
    
}