using System.Text.RegularExpressions;
using DemoExam.Domain.Models;
using FluentValidation;

namespace DemoExam.Domain.Validators;

public partial class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x => x.ArticleNumber).NotEmpty();
        RuleFor(x => x.ArticleNumber).Length(6);
        RuleFor(x => x.ArticleNumber).Must(x => x is not null && !CyrillicRegex().IsMatch(x));
        RuleFor(x => x.ArticleNumber).Must(x => x?.All(char.IsDigit) is false);
        RuleFor(x => x.ArticleNumber).Must(x => x?.All(char.IsLetter) is false);
        RuleFor(x => x.ArticleNumber).Must(x => x?.Any(char.IsLower) is false);

        RuleFor(x => x.Name).NotEmpty();
        
        RuleFor(x => x.Description).NotEmpty();
        
        RuleFor(x => x.Category).NotEmpty();
        
        RuleFor(x => x.QuantityInStock).GreaterThanOrEqualTo(0);
        
        RuleFor(x => x.Discount).GreaterThanOrEqualTo((byte)0);
        RuleFor(x => x.Discount).LessThanOrEqualTo((byte)100);

        RuleFor(x => x.ManufacturerName).NotEmpty();
        
        RuleFor(x => x.SupplierName).NotEmpty();

        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
    }

    [GeneratedRegex("\\p{IsCyrillic}")]
    private static partial Regex CyrillicRegex();
}