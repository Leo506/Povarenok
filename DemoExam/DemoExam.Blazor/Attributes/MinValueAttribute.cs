using System.ComponentModel.DataAnnotations;

namespace DemoExam.Blazor.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class MinValueAttribute : ValidationAttribute
{
    private readonly double _minValue;

    public MinValueAttribute(double minValue)
    {
        _minValue = minValue;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"Поле должно быть минимум {_minValue}";
    }

    public override bool IsValid(object? value)
    {
        return value switch
        {
            double number => number >= _minValue,
            int number => number >= _minValue,
            decimal number => number >= Convert.ToDecimal(_minValue),
            _ => false
        };
    }
}