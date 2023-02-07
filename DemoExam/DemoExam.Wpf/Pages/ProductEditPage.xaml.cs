using DemoExam.Core.ViewModels;
using MvvmCross.Platforms.Wpf.Views;

namespace DemoExam.Wpf.Pages;

public partial class ProductEditPage : MvxWindow<ProductEditViewModel>
{
    public ProductEditPage()
    {
        InitializeComponent();
    }
}