using DemoExam.Domain.Models;

namespace DemoExam.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetUser(string login, string password);
    Task<User?> GetUser(string login);
    Task CreateUser(string login, string password, string userName, string userSurname, string userPatronymic);
}