using Microsoft.JSInterop;

namespace DemoExam.Blazor.Services.Modals;

public class ModalService : IModalService
{
    private readonly IJSRuntime _jsRuntime;

    public ModalService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task CloseModal()
    {
        await _jsRuntime.InvokeVoidAsync("closeModal");
    }
}