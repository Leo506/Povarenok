using System.Windows.Controls;
using DemoExam.Core.ViewModels.ProductsViewModel;
using DemoExam.Wpf.DataElements;
using MvvmCross.Platforms.Wpf.Views;

namespace DemoExam.Wpf.Pages;

public abstract class ProductsPage : MvxWpfView<ProductsViewModelBase>
{
    protected virtual void OnSearchTextChange(object sender, TextChangedEventArgs e)
    {
        ViewModel.SearchString = (sender as TextBox)!.Text;
    }

    protected virtual void OnDiscountSelectorChange(object sender, SelectionChangedEventArgs e)
    {
        if ((sender as ComboBox)?.SelectedItem is DiscountSelectableItem discountSelectableItem)
            ViewModel?.ChangeDiscountSelector(discountSelectableItem.GetDiscountSortPredicate());
    }
}