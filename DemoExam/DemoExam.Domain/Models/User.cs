namespace DemoExam.Domain.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserSurname { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string UserPatronymic { get; set; } = null!;

    public string UserLogin { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public int UserRole { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual Role UserRoleNavigation { get; set; } = null!;
}
