using DemoExam.Core.Repositories;
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
}