namespace DemoExam.Blazor.Services.LocalStorage;

public interface ILocalStorageService
{
    Task SetAsync<TValue>(string key, TValue value) where TValue : class;

    Task<TValue?> GetAsync<TValue>(string key) where TValue : class;

    Task RemoveAsync(string key);
}