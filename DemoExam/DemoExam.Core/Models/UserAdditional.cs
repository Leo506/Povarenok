using System.ComponentModel.DataAnnotations.Schema;

namespace DemoExam.Core.Models;

public partial class User
{
    [NotMapped] public string FullName => $"{UserSurname} {UserName} {UserPatronymic}";

    public static User Guest => new User()
    {
        UserLogin = "Guest",
        UserName = "Guest"
    };
}