using System.ComponentModel.DataAnnotations;

namespace DemoExam.Blazor.ViewModels;

public class RegistrationViewModel
{
    [Required] public string UserName { get; set; } = default!;
    [Required] public string UserSurname { get; set; } = default!;
    [Required] public string UserPatronymic { get; set; } = default!;
    [Required] public string Login { get; set; } = default!;
    [Required] public string Password { get; set; } = default!;
}