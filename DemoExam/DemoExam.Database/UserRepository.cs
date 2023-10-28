using DemoExam.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoExam.Database;

public partial class TradeContext : IUserRepository
{
    public Task<User?> GetUser(string login, string password)
    {
        return Users
            .Include(x => x.UserRoleNavigation)
            .FirstOrDefaultAsync(x => x.UserLogin == login && x.UserPassword == password);
    }

    public Task<User?> GetUser(string login)
    {
        return Users
            .Include(x => x.UserRoleNavigation)
            .FirstOrDefaultAsync(x => x.UserLogin == login);
    }

    public async Task CreateUser(string login, string password, string userName, string userSurname, string userPatronymic)
    {
        var clientRoleId = await Roles.FirstAsync(x => x.RoleName == Role.ClientRoleName).ConfigureAwait(false);
        await Users.AddAsync(new User()
        {
            UserLogin = login,
            UserPassword = password,
            UserName = userName,
            UserSurname = userSurname,
            UserPatronymic = userPatronymic,
            UserRole = clientRoleId.Id
        })
            .ConfigureAwait(false);
        await SaveChangesAsync().ConfigureAwait(false);
    }
}