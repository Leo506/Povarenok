using DemoExam.Core.ViewModels.ProductsViewModel;
using MvvmCross.Platforms.Wpf.Views;

namespace DemoExam.Wpf.Pages;

public partial class ClientProductsPage : MvxWpfView<ClientProductsViewModel>
{
    public ClientProductsPage()
    {
        InitializeComponent();
    }
}