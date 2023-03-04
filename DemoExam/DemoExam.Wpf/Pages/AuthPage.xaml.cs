using System.Windows;
using System.Windows.Controls;
using DemoExam.Core.ViewModels;
using MvvmCross.Platforms.Wpf.Views;

namespace DemoExam.Wpf.Pages;

public partial class AuthPage : MvxWpfView<AuthViewModel>
{
    public AuthPage()
    {
        InitializeComponent();
    }

    private void OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        ViewModel.Password = ((PasswordBox)sender).Password;
    }
}