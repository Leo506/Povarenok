using DemoExam.Core.Models;
using DemoExam.Domain.Entities;

namespace DemoExam.Core.Extensions;

public static class UserModelExtensions
{
    public static bool IsClientOrGuest(this UserModel user)
    {
        return user.Role is Role.ClientRoleName;
    }

    public static bool IsManager(this UserModel user)
    {
        return user.Role is Role.ManagerRoleName;
    }

    public static bool IsAdmin(this UserModel user)
    {
        return user.Role is Role.AdminRoleName;
    }
}