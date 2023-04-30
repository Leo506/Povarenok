using System.ComponentModel.DataAnnotations.Schema;

namespace DemoExam.Domain.Models;

public partial class User
{
    [NotMapped] public string FullName => $"{UserSurname} {UserName} {UserPatronymic}";

    public static Domain.Models.User Guest => new()
    {
        UserLogin = "Guest",
        UserName = "Guest",
        UserRoleNavigation = new Domain.Models.Role
        {
            RoleName = Domain.Models.Role.ClientRoleName
        }
    };
}