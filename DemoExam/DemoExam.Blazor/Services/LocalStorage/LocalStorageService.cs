using System.Text.Json;
using Microsoft.JSInterop;

namespace DemoExam.Blazor.Services.LocalStorage;

public class LocalStorageService : ILocalStorageService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly ILogger<LocalStorageService> _logger;

    public LocalStorageService(IJSRuntime jsRuntime, ILogger<LocalStorageService> logger)
    {
        _jsRuntime = jsRuntime;
        _logger = logger;
    }


    public async Task SetAsync<TValue>(string key, TValue value) where TValue : class
    {
        var data = JsonSerializer.Serialize(value);
        await _jsRuntime.InvokeVoidAsync("set", key, data);
    }

    public async Task<TValue?> GetAsync<TValue>(string key) where TValue : class
    {
        try
        {
            var result = await _jsRuntime.InvokeAsync<string>("get", key);
            return string.IsNullOrEmpty(result) 
                ? null 
                : JsonSerializer.Deserialize<TValue>(result)!;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get value from local storage");
        }

        return null;
    }

    public async Task RemoveAsync(string key)
    {
        await _jsRuntime.InvokeAsync<string>("remove", key);
    }
}