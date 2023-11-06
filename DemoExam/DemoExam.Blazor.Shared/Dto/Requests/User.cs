using System.ComponentModel.DataAnnotations;

namespace DemoExam.Blazor.Shared.Dto.Requests;

public record User(
    [Required] string UserName,
    [Required] string UserSurname,
    [Required] string UserPatronymic,
    [Required] string Login,
    [Required] string Password
);