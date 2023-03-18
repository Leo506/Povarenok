using System.Windows.Controls;
using DemoExam.Core.ViewModels.ProductsViewModel;
using DemoExam.Wpf.DataElements;
using MvvmCross.Platforms.Wpf.Views;

namespace DemoExam.Wpf.Pages;

public partial class ProductsPage : MvxWpfView<ProductsViewModelBase>
{
    public ContextMenu ProductsListContextMenu
    {
        get => ProductsList.ContextMenu;
        set => ProductsList.ContextMenu = value;
    }
    
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
            ViewModel?.ChangeDiscountSelector(discountSelectableItem.GetDiscountSortPredicate());
    }
}