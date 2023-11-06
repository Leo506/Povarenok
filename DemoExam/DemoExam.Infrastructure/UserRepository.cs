using DemoExam.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoExam.Infrastructure;

internal partial class UserRepository : IUserRepository
{
    private readonly TradeContext _tradeContext;

    public UserRepository(TradeContext tradeContext)
    {
        _tradeContext = tradeContext;
    }

    public Task<User?> GetUser(string login, string password)
    {
        return _tradeContext.Users
            .Include(x => x.UserRoleNavigation)
            .FirstOrDefaultAsync(x => x.Login == login && x.Password == password);
    }

    public Task<User?> GetUser(string login)
    {
        return _tradeContext.Users
            .Include(x => x.UserRoleNavigation)
            .FirstOrDefaultAsync(x => x.Login == login);
    }

    public async Task CreateUser(string login, string password, string userName, string userSurname, string userPatronymic)
    {
        var clientRoleId = await _tradeContext.Roles.FirstAsync(x => x.Name == Role.ClientRoleName).ConfigureAwait(false);
        await _tradeContext.Users.AddAsync(new User()
        {
            Login = login,
            Password = password,
            Name = userName,
            Surname = userSurname,
            Patronymic = userPatronymic,
            RoleId = clientRoleId.Id
        })
            .ConfigureAwait(false);
        await _tradeContext.SaveChangesAsync().ConfigureAwait(false);
    }
}