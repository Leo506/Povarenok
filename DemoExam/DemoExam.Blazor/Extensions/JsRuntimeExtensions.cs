using Microsoft.JSInterop;

namespace DemoExam.Blazor.Extensions;

public static class JsRuntimeExtensions
{
    public static async Task ShowModal(this IJSRuntime jsRuntime, string id) =>
        await jsRuntime.InvokeVoidAsync("showModal", id);

    public static async Task CloseModal(this IJSRuntime jsRuntime) => 
        await jsRuntime.InvokeVoidAsync("closeModal");

    public static async Task<bool> Confirm(this IJSRuntime jsRuntime, string message) =>
        await jsRuntime.InvokeAsync<bool>("confirm", message);
}