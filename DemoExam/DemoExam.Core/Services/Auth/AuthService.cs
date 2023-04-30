
using System.Security.Authentication;
using DemoExam.Core.Repositories;

namespace DemoExam.Core.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUserRepository _repository;

    public AuthService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<User> AuthenticateAsync(string login, string password)
    {
        var user = await _repository.GetUser(login, password).ConfigureAwait(false);
        if (user is null)
            throw new AuthenticationException("Incorrect login or password");

        return user;
    }
}