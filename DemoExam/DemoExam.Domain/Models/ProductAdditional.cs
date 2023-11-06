using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoExam.Domain.Validators;

namespace DemoExam.Domain.Models;

public partial class Product
{
    private string? _manufacturerName;
    private string? _supplierName;
    
    [NotMapped]
    [Required]
    public string ManufacturerName
    {
        get => _manufacturerName ??= Manufacturer.Name;
        set => _manufacturerName = value;
    }

    [NotMapped]
    [Required]
    public string SupplierName
    {
        get => _supplierName ??= Supplier.Name;
        set => _supplierName = value;
    }
    
    [NotMapped]
    public decimal ProductCostWithDiscount => Discount == 0 ? Price : Price - Price * (Discount / 100.0m);

    public void Update(Product product)
    {
        ArticleNumber = product.ArticleNumber;
        Name = product.Name;
        Description = product.Description;
        Category = product.Category;
        Photo = product.Photo;
        Price = product.Price;
        QuantityInStock = product.QuantityInStock;
        Discount = product.Discount;
        ManufacturerId = product.ManufacturerId;
        SupplierId = product.SupplierId;
        ManufacturerName = product.ManufacturerName;
        SupplierName = product.SupplierName;
    }

    public void Validate()
    {
        var validator = new ProductValidator();

        var result = validator.Validate(this);
        
        if (result.IsValid)
            return;

        throw new InvalidDataException(string.Join("\n", result.Errors.DistinctBy(x => x.ErrorMessage)));
    }
}