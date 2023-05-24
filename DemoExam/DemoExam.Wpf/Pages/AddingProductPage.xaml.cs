using System.Windows;
using DemoExam.Core.Utils;
using DemoExam.Core.ViewModels;
using DemoExam.Wpf.Utils;
using Microsoft.Win32;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;

namespace DemoExam.Wpf.Pages;

[MvxWindowPresentation(Modal = true)]
public partial class AddingProductPage : MvxWindow<AddingProductViewModel>
{
    public AddingProductPage()
    {
        InitializeComponent();
    }

    private void OnChangePhotoClick(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "Image files|*.bmp;*.jpeg;*.jpg;*.png",
            FilterIndex = 1
        };

        if (openFileDialog.ShowDialog() is not true) return;

        var byteArray = FileToByteArrayConverter.Convert(openFileDialog.FileName);

        ViewModel.SetProductPhoto(byteArray);

        ProductPhoto.Source = BytesToBitmapConverter.Convert(byteArray);
    }
}