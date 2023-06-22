using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DemoExam.Domain.Attributes;

public class ProductArticleNumberAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not string articleNumber)
            return false;

        if (articleNumber.Length != 6)
            return false;
        
        if (articleNumber.Any(char.IsLower))
        {
            return false;
        }

        if (Regex.IsMatch(articleNumber, @"\p{IsCyrillic}"))
        {
            return false;
        }

        return true;
    }
}