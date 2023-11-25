using Arbus.Network.Exceptions;
using BlazorBootstrap;

namespace DemoExam.Blazor.Extensions;

public static class ToastServiceExtensions
{
    public static void FromException(this ToastService toastService, Exception exception)
    {
        if (exception is NetworkException)
            toastService.ToastForNetworkException();
        else
            toastService.ToastUnexpectedException();
    }
    
    public static void ToastForNetworkException(this ToastService toastService)
    {
        toastService.Notify(new ToastMessage()
        {
            Type = ToastType.Danger,
            IconName = IconName.EmojiFrown,
            Title = "Ошибка",
            Message = "При обработке запроса произошла ошибка. Повторите попытку позже"
        });
    }

    public static void ToastUnexpectedException(this ToastService toastService)
    {
        toastService.Notify(new ToastMessage()
        {
            
            Type = ToastType.Danger,
            IconName = IconName.EmojiFrown,
            Title = "Ошибка",
            Message = "Произошла неожиданная ошибка, но мы ее уже чиним"
        });
    }
}