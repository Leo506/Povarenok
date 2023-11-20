using System.ComponentModel.DataAnnotations;

namespace DemoExam.Blazor.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Поле обязательно")]
    public string Login { get; set; } = default!;

    [Required(ErrorMessage = "Поле обязательно")]
    public string Password { get; set; } = default!;
}