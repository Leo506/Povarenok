using MvvmCross.ViewModels;

namespace DemoExam.Core.ObservableObjects;

public class ObservableOrder : MvxNotifyPropertyChanged
{
    private int _amount;
    public Product ObservableProduct { get; set; } = default!;

    public int Amount
    {
        get => _amount;
        set => SetProperty(ref _amount, value);
    }
}