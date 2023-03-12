namespace DemoExam.Core.Models;

public class UserModel
{
    public string UserFullName { get; set; } = default!;

    public string Role { get; set; } = default!;

    public static UserModel Guest => new()
    {
        UserFullName = "Guest",
        Role = Domain.Entities.Role.ClientRoleName
    };
}