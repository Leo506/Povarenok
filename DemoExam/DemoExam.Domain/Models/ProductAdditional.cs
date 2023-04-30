using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    
    public void Validate()
    {
        var validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(this, new ValidationContext(this), validationResults, true);
        
        if (validationResults.Count == 0) return;

        throw new InvalidDataException(string.Join("\n", validationResults));
    }
}