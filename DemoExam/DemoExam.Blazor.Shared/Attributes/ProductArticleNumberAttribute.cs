using System.ComponentModel.DataAnnotations;
using DemoExam.Blazor.Shared.Validators;

namespace DemoExam.Blazor.Shared.Attributes;

public class ProductArticleNumberAttribute : ValidationAttribute
{
    public override string FormatErrorMessage(string name) => $"{name} is not in article number format";

    public override bool IsValid(object? value)
    {
        if (value is not string article)
            return false;
        var validator = new ArticleNumberValidator();
        return validator.Validate(article).IsValid;
    }
}