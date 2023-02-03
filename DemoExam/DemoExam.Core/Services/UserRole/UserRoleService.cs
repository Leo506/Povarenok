using DemoExam.Core.Models;

namespace DemoExam.Core.Services.UserRole;

public class UserRoleService : IUserRoleService
{
    public bool IsClientOrGuest(User user) => user.UserRoleNavigation.RoleName == Role.ClientRoleName;

    public bool IsManager(User user) => user.UserRoleNavigation.RoleName == Role.ManagerRoleName;

    public bool IsAdmin(User user) => user.UserRoleNavigation.RoleName == Role.AdminRoleName;
}