namespace DemoExam.Blazor.Shared.Dto.Responses;

public record Product
{
    public string ArticleNumber { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string? Photo { get; set; }

    public decimal Price { get; set; }
    
    public int QuantityInStock { get; set; }
    
    public byte Discount { get; set; }

    public string ManufacturerName { get; set; } = default!;

    public string SupplierName { get; set; } = default!;
}