namespace DemoExam.Blazor.Services.AccessToken;

public interface IAccessTokenService
{
    Task<string?> GetAccessToken();

    Task SetAccessToken(string accessToken);

    Task RemoveAccessToken();
}