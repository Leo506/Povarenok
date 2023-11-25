using System.ComponentModel.DataAnnotations;
using DemoExam.Domain.Models;

namespace DemoExam.Blazor.Shared.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class OrderStatusAttribute: ValidationAttribute
{
    public override string FormatErrorMessage(string name)
    {
        return $"Order status must be \"{OrderStatusConstants.NewOrder}\" or \"{OrderStatusConstants.Completed}\"";
    }

    public override bool IsValid(object? value)
    {
        return value is OrderStatusConstants.NewOrder or OrderStatusConstants.Completed or null;
    }
}