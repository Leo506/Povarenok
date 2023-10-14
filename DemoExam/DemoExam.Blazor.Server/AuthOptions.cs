using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DemoExam.Blazor.Server;

public class AuthOptions
{
    public const string Issuer = "PovarenokAPI";
    
    private const string Key = "07A55064-4881-4A93-A048-B0A14CE224EB";
    
    public static TimeSpan LifeTime => TimeSpan.FromMinutes(1);

    public static SymmetricSecurityKey SecurityKey => new(Encoding.UTF8.GetBytes(Key));
}