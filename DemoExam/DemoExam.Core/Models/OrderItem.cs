using DemoExam.Core.NotifyObjects;
using MvvmCross.ViewModels;

namespace DemoExam.Core.Models;

public class OrderItem : MvxNotifyPropertyChanged
{
    public ProductNotifyObject Product { get; set; } = default!;

    private int _amount;
    public int Amount
    {
        get => _amount;
        set => SetProperty(ref _amount, value);
    }
}