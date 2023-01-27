using System.Windows.Controls;
using DemoExam.Core.ViewModels;
using MvvmCross.Platforms.Wpf.Views;

namespace DemoExam.Wpf.Pages;

public partial class ProductsPage : MvxWpfView<ProductsViewModel>
{
    public ProductsPage()
    {
        InitializeComponent();
    }
}