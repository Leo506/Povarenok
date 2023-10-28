using System.Security.Authentication;
using DemoExam.Domain.Exceptions;
using DemoExam.Domain.Models;
using DemoExam.Domain.Repositories;

namespace DemoExam.Domain.Services.Auth;

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

    public async Task RegisterUser(string login, string password, string userName, string userSurname, string userPatronymic)
    {
        var user = await _repository.GetUser(login).ConfigureAwait(false);
        if (user is not null)
            throw new DuplicateLoginException();

        await _repository.CreateUser(login, password, userName, userSurname, userPatronymic).ConfigureAwait(false);
    }
}