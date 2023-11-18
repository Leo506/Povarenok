using System.ComponentModel.DataAnnotations;
using DemoExam.Blazor.Shared.Attributes;

namespace DemoExam.Blazor.Shared.Dto.Requests;

public class NewProduct
{
    [Required]
    [ProductArticleNumber]
    public required string ArticleNumber { get; set; }
    
    [Required]
    public required string Name { get; set; }

    [Required]
    public required string? Description { get; set; }

    [Required]
    public required string? Category { get; set; }
    
    [Base64String(allowEmpty:true)]
    public string? Photo { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public required decimal Price { get; set; }
    
    [Required]
    [Range(0, int.MaxValue)]
    public required int QuantityInStock { get; set; }
    
    [Range(0, 100)]
    public byte? Discount { get; set; }

    [Required]
    public required string ManufacturerName { get; set; }

    [Required]
    public required string SupplierName { get; set; }
}