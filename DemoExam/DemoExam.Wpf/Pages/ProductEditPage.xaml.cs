using System.Windows;
using DemoExam.Core.Utils;
using DemoExam.Core.ViewModels;
using DemoExam.Wpf.Utils;
using Microsoft.Win32;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;

namespace DemoExam.Wpf.Pages;

[MvxWindowPresentation(Modal = true)]
public partial class ProductEditPage : MvxWindow<ProductEditViewModel>
{
    public ProductEditPage()
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

        ViewModel.UpdateProductPhoto(byteArray);

        ProductPhoto.Source = BytesToBitmapConverter.Convert(byteArray);
    }
}