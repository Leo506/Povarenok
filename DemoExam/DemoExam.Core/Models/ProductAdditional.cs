using System.ComponentModel.DataAnnotations.Schema;

namespace DemoExam.Core.Models;

public partial class Product
{
    [NotMapped]
    public decimal ActualProductCost =>
        CurrentDiscount == 0 ? ProductCost : ProductCost - ProductCost * (CurrentDiscount / 100.0m);
}