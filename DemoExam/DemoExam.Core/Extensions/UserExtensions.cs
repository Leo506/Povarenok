using DemoExam.Core.Models;

namespace DemoExam.Core.Extensions;

public static class UserExtensions
{
    public static bool IsClientOrGuest(this User user)
    {
        return user.UserRoleNavigation.RoleName is Role.ClientRoleName or "Guest";
    }

    public static bool IsManager(this User user)
    {
        return user.UserRoleNavigation.RoleName is Role.ManagerRoleName;
    }

    public static bool IsAdmin(this User user)
    {
        return user.UserRoleNavigation.RoleName is Role.AdminRoleName;
    }
}