using System.ComponentModel.DataAnnotations.Schema;
using DemoExam.Translation;

namespace DemoExam.Domain.Models;

public partial class User
{
    [NotMapped] public string FullName => $"{UserSurname} {UserName} {UserPatronymic}";

    public static User Guest => new()
    {
        UserLogin = Translate.Guest,
        UserName = Translate.Guest,
        UserRoleNavigation = new Role
        {
            RoleName = Role.ClientRoleName
        }
    };
}