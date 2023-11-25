using System.ComponentModel.DataAnnotations;
using DemoExam.Blazor.Attributes;
using DemoExam.Blazor.Shared.Dto.Requests;
using DemoExam.Blazor.Shared.Dto.Responses;

namespace DemoExam.Blazor.ViewModels;

public class ProductEditViewModel
{
    private const string PropertyMustBeFilled = "Поле должно быть заполнено";
    
    public  string ArticleNumber { get; set; }
    
    [Required(ErrorMessage = PropertyMustBeFilled)]
    public  string Name { get; set; } = default!;

    [Required(ErrorMessage = PropertyMustBeFilled)]
    public  string Description { get; set; } = null!;

    [Required(ErrorMessage = PropertyMustBeFilled)]
    public  string Category { get; set; } = null!;

    public  string? Photo { get; set; }

    [MinValue(1)]
    [Required(ErrorMessage = PropertyMustBeFilled)]
    public  decimal Price { get; set; }
    
    [MinValue(0)]
    [Required(ErrorMessage = PropertyMustBeFilled)]
    public  int QuantityInStock { get; set; }
    
    [Range(0, 100, ErrorMessage = "Поле должно быть от 0 до 100")]
    public  int? Discount { get; set; }

    [Required(ErrorMessage = PropertyMustBeFilled)]
    public  string ManufacturerName { get; set; } = default!;

    [Required(ErrorMessage = PropertyMustBeFilled)]
    public  string SupplierName { get; set; } = default!;

    public ProductEditViewModel(Product product)
    {
        ArticleNumber = product.ArticleNumber;
        Name = product.Name;
        Description = product.Description;
        Category = product.Category;
        Price = product.Price;
        Discount = product.Discount;
        QuantityInStock = product.QuantityInStock;
        ManufacturerName = product.ManufacturerName;
        SupplierName = product.SupplierName;
        Photo = product.Photo;
    }

    public ProductEdit ToDto()
    {
        return new()
        {
            Name = Name,
            Description = Description,
            Category = Category,
            Price = Price,
            Discount = Convert.ToByte(Discount),
            QuantityInStock = QuantityInStock,
            ManufacturerName = ManufacturerName,
            SupplierName = SupplierName,
            Photo = Photo
        };
    }
}