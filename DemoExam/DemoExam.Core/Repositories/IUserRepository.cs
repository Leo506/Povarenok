namespace DemoExam.Core.Repositories;

public interface IUserRepository
{
    Task<User?> GetUser(string login, string password);
}