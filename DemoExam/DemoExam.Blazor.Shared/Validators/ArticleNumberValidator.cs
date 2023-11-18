using System.Text.RegularExpressions;
using FluentValidation;

namespace DemoExam.Blazor.Shared.Validators;

public partial class ArticleNumberValidator : AbstractValidator<string>
{
    public ArticleNumberValidator()
    {
        RuleFor(x => x).NotEmpty();
        RuleFor(x => x).Length(6);
        RuleFor(x => x).Must(x => x is not null && !CyrillicRegex().IsMatch(x));
        RuleFor(x => x).Must(x => x?.All(char.IsDigit) is false);
        RuleFor(x => x).Must(x => x?.All(char.IsLetter) is false);
        RuleFor(x => x).Must(x => x?.Any(char.IsLower) is false);
    }
    
    [GeneratedRegex("\\p{IsCyrillic}")]
    private static partial Regex CyrillicRegex();
}