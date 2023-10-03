using DemoExam.Domain.Models;

namespace DemoExam.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetUser(string login, string password);
}