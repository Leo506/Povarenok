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
        get => _manufacturerName ??= Manufacturer.ManufacturerName;
        set => _manufacturerName = value;
    }

    [NotMapped]
    [Required]
    public string SupplierName
    {
        get => _supplierName ??= Supplier.SupplierName;
        set => _supplierName = value;
    }
    
    [NotMapped]
    public decimal ProductCostWithDiscount => CurrentDiscount == 0 ? ProductCost : ProductCost - ProductCost * (CurrentDiscount / 100.0m);

    public Product(Product product)
    {
        ProductArticleNumber = product.ProductArticleNumber;
        ProductName = product.ProductName;
        ProductDescription = product.ProductDescription;
        ProductCategory = product.ProductCategory;
        ProductPhoto = product.ProductPhoto;
        ProductCost = product.ProductCost;
        MaxDiscount = product.MaxDiscount;
        ProductQuantityInStock = product.ProductQuantityInStock;
        CurrentDiscount = product.CurrentDiscount;
        ManufacturerId = product.ManufacturerId;
        SupplierId = product.SupplierId;
        Manufacturer = product.Manufacturer;
        OrderLists = product.OrderLists;
        Supplier = product.Supplier;
        ManufacturerName = product.ManufacturerName;
        SupplierName = product.SupplierName;
    }

    public void Update(Product product)
    {
        ProductArticleNumber = product.ProductArticleNumber;
        ProductName = product.ProductName;
        ProductDescription = product.ProductDescription;
        ProductCategory = product.ProductCategory;
        ProductPhoto = product.ProductPhoto;
        ProductCost = product.ProductCost;
        MaxDiscount = product.MaxDiscount;
        ProductQuantityInStock = product.ProductQuantityInStock;
        CurrentDiscount = product.CurrentDiscount;
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