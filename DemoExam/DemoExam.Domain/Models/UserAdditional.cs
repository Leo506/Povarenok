using System.ComponentModel.DataAnnotations.Schema;

namespace DemoExam.Domain.Models;

public partial class User
{
    [NotMapped] public string FullName => $"{Surname} {Name} {Patronymic}".Trim();
}