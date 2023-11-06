using System.Net.Http.Headers;
using Arbus.Network;
using DemoExam.Blazor.Shared.Dto.Responses;

namespace DemoExam.Blazor.Network.Endpoints;

public class LoginEndpoint : ApiEndpoint<Login>
{
    private readonly string _login;
    private readonly string _password;
    
    public LoginEndpoint(string login, string password) : base(HttpMethod.Get, "Auth/Login")
    {
        _login = login;
        _password = password;
    }

    protected override void AddRequestHeaders(HttpRequestHeaders headers)
    {
        base.AddRequestHeaders(headers);
        headers.Add("login", _login);
        headers.Add("password", _password);
    }
}