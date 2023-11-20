using System.ComponentModel.DataAnnotations;
using DemoExam.Blazor.Shared.Attributes;

namespace DemoExam.Blazor.Shared.Dto.Requests;

public class NewProduct
{
    [Required] [ProductArticleNumber] public string ArticleNumber { get; set; } = default!;

    [Required] public string Name { get; set; } = default!;

    [Required]
    public string? Description { get; set; }

    [Required]
    public string? Category { get; set; }
    
    [Base64String(allowEmpty:true)]
    public string? Photo { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }
    
    [Required]
    [Range(0, int.MaxValue)]
    public int QuantityInStock { get; set; }
    
    [Range(0, 100)]
    public int? Discount { get; set; }

    [Required] public string ManufacturerName { get; set; } = default!;

    [Required] public string SupplierName { get; set; } = default!;
}