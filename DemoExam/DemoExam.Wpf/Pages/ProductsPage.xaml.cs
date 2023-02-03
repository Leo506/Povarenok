using System.Collections.Generic;
using System.Windows.Controls;
using DemoExam.Core.ViewModels;
using DemoExam.Wpf.DataElements;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace DemoExam.Wpf.Pages;

public partial class ProductsPage : MvxWpfView<ProductsViewModel>
{
    public ProductsPage()
    {
        InitializeComponent();
    }

    private void OnSearchTextChange(object sender, TextChangedEventArgs e)
    {
        ViewModel.SearchString = (sender as TextBox)!.Text;
    }

    private void OnDiscountSelectorChange(object sender, SelectionChangedEventArgs e)
    {
        if ((sender as ComboBox)?.SelectedItem is DiscountSelectableItem discountSelectableItem)
        {
            ViewModel?.ChangeDiscountSelector(discountSelectableItem.GetDiscountSortPredicate());
        }
    }
}