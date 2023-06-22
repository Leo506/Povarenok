using System.Text.RegularExpressions;
using DemoExam.Domain.Models;
using DemoExam.Translation;
using FluentValidation;

namespace DemoExam.Domain.Validators;

public partial class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x => x.ProductArticleNumber).NotEmpty().WithMessage(Translate.ProductArticleNumberIsRequired);
        RuleFor(x => x.ProductArticleNumber).Length(6).WithMessage(Translate.ProductArticlleNumberMustHaveSixLetters);
        RuleFor(x => x.ProductArticleNumber).Must(x => x is not null && !CyrillicRegex().IsMatch(x))
            .WithMessage(Translate.ProductArticleNumberMustContainsDigitsAndUppercaseLatinLetters);
        RuleFor(x => x.ProductArticleNumber).Must(x => x?.All(char.IsDigit) is false)
            .WithMessage(Translate.ProductArticleNumberMustContainsDigitsAndUppercaseLatinLetters);
        RuleFor(x => x.ProductArticleNumber).Must(x => x?.All(char.IsLetter) is false)
            .WithMessage(Translate.ProductArticleNumberMustContainsDigitsAndUppercaseLatinLetters);
        RuleFor(x => x.ProductArticleNumber).Must(x => x?.Any(char.IsLower) is false)
            .WithMessage(Translate.ProductArticleNumberMustContainsDigitsAndUppercaseLatinLetters);

        RuleFor(x => x.ProductName).NotEmpty().WithMessage(Translate.ProductNameIsRequired);
        RuleFor(x => x.ProductDescription).NotEmpty().WithMessage(Translate.ProductDescriptionIsRequired);
        RuleFor(x => x.ProductCategory).NotEmpty().WithMessage(Translate.ProductCategoryIsRequired);
        RuleFor(x => x.ProductQuantityInStock).GreaterThanOrEqualTo(0)
            .WithMessage(Translate.ProductQuantityInStockMustBeMoreOrEqualToZero);
        RuleFor(x => x.CurrentDiscount).GreaterThanOrEqualTo((byte)0)
            .WithMessage(Translate.CurrentDiscountMustBeMoreOrEqualToZero);

        RuleFor(x => x.ManufacturerName).NotEmpty().WithMessage(Translate.ManufacturerIsRequired);
        RuleFor(x => x.SupplierName).NotEmpty().WithMessage(Translate.SupplierIsRequired);
    }

    [GeneratedRegex("\\p{IsCyrillic}")]
    private static partial Regex CyrillicRegex();
}