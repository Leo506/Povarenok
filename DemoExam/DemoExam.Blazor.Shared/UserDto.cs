using System.ComponentModel.DataAnnotations;

namespace DemoExam.Blazor.Shared;

public record UserDto(
    [Required] string UserName,
    [Required] string UserSurname,
    [Required] string UserPatronymic,
    [Required] string Login,
    [Required] string Password
);