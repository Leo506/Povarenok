using DemoExam.Core.Models;

namespace DemoExam.Core.Services.UserRole;

public interface IUserRoleService
{
    bool IsClientOrGuest(User user);
    bool IsManager(User user);
    bool IsAdmin(User user);
}