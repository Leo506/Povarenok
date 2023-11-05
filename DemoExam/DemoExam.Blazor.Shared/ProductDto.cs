namespace DemoExam.Blazor.Shared;

public record ProductDto
{
    public string ProductArticleNumber { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string ProductDescription { get; set; } = null!;

    public string ProductCategory { get; set; } = null!;

    public string? ProductPhoto { get; set; }

    public decimal ProductCost { get; set; }

    public byte? MaxDiscount { get; set; }
    
    public int ProductQuantityInStock { get; set; }
    
    public byte CurrentDiscount { get; set; }

    public string ManufacturerName { get; set; } = default!;

    public string SupplierName { get; set; } = default!;
}