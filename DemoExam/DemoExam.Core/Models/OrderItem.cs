using DemoExam.Core.NotifyObjects;
using MvvmCross.ViewModels;

namespace DemoExam.Core.Models;

public class OrderItem : MvxNotifyPropertyChanged
{
    private int _amount;
    public ProductNotifyObject Product { get; set; } = default!;

    public int Amount
    {
        get => _amount;
        set => SetProperty(ref _amount, value);
    }
}