using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;

namespace DemoExam.Core.Models;

public partial class Product
{
    public void Validate()
    {
        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(this, new ValidationContext(this), validationResults, true);
        
        if (validationResults.IsNullOrEmpty()) return;

        throw new InvalidDataException(string.Join("\n", validationResults));
    }
}