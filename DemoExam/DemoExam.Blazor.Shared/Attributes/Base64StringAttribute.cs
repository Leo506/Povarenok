using System.ComponentModel.DataAnnotations;

namespace DemoExam.Blazor.Shared.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class Base64StringAttribute : ValidationAttribute
{
    private readonly bool _allowEmpty;

    public Base64StringAttribute(bool allowEmpty = false)
    {
        _allowEmpty = allowEmpty;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"{name} is not in Base64 format";
    }

    public override bool IsValid(object? value)
    {
        if (_allowEmpty && string.IsNullOrEmpty(value as string))
            return true;
        if (value is not string str)
            return false;
        var buffer = new Span<byte>(new byte[str.Length]);
        return Convert.TryFromBase64String(str, buffer, out _);
    }
}