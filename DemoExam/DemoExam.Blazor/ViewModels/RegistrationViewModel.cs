using System.ComponentModel.DataAnnotations;

namespace DemoExam.Blazor.ViewModels;

public class RegistrationViewModel
{
    [Required(ErrorMessage = "Поле обязательно")] public string UserName { get; set; } = default!;
    [Required(ErrorMessage = "Поле обязательно")] public string UserSurname { get; set; } = default!;
    [Required(ErrorMessage = "Поле обязательно")] public string UserPatronymic { get; set; } = default!;
    [Required(ErrorMessage = "Поле обязательно")] public string Login { get; set; } = default!;
    [Required(ErrorMessage = "Поле обязательно")] public string Password { get; set; } = default!;
}