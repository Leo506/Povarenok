using DemoExam.Core.ViewModels;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;

namespace DemoExam.Wpf.Pages;

[MvxWindowPresentation(Modal = true)]
public partial class OrderPage : MvxWindow<OrderViewModel>
{
    public OrderPage()
    {
        InitializeComponent();
    }
}