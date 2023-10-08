using System.ComponentModel.DataAnnotations;

namespace DemoExam.Blazor.ViewModels;

public class LoginViewModel
{
    [Required]
    public string Login { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}