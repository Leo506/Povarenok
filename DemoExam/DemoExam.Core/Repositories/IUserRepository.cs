using DemoExam.Core.Models;

namespace DemoExam.Core.Repositories;

public interface IUserRepository
{
    Task<UserModel?> GetUser(string login, string password);
}