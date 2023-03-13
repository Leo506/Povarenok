using DemoExam.Core.Models;
using MvvmCross.ViewModels;

namespace DemoExam.Core.NotifyObjects;

public class ProductNotifyObject : MvxNotifyPropertyChanged
{
    public ProductNotifyObject(Product product)
    {
        Product = product;
    }

    public string ProductArticleNumber
    {
        get => Product.ProductArticleNumber;
        set
        {
            Product.ProductArticleNumber = value;
            RaisePropertyChanged(() => ProductArticleNumber);
        }
    }

    public string ProductName
    {
        get => Product.ProductName;
        set
        {
            Product.ProductName = value;
            RaisePropertyChanged(() => ProductName);
        }
    }

    public string ProductDescription
    {
        get => Product.ProductDescription;
        set
        {
            Product.ProductDescription = value;
            RaisePropertyChanged(() => ProductDescription);
        }
    }

    public string ProductCategory
    {
        get => Product.ProductCategory;
        set
        {
            Product.ProductCategory = value;
            RaisePropertyChanged(() => ProductCategory);
        }
    }

    public byte[]? ProductPhoto
    {
        get => Product.ProductPhoto;
        set
        {
            Product.ProductPhoto = value;
            RaisePropertyChanged(() => ProductPhoto);
        }
    }

    public string ProductManufacturer
    {
        get => Product.ProductManufacturer;
        set
        {
            Product.ProductManufacturer = value;
            RaisePropertyChanged(() => ProductManufacturer);
        }
    }

    public decimal ProductCost
    {
        get => Product.ProductCost;
        set
        {
            Product.ProductCost = value;
            RaisePropertyChanged(() => ProductCost);
            RaisePropertyChanged(() => ProductCostWithDiscount);
        }
    }

    public int ProductQuantityInStock
    {
        get => Product.ProductQuantityInStock;
        set
        {
            Product.ProductQuantityInStock = value;
            RaisePropertyChanged(() => ProductQuantityInStock);
        }
    }

    public byte CurrentDiscount
    {
        get => Product.CurrentDiscount;
        set
        {
            Product.CurrentDiscount = value;
            RaisePropertyChanged(() => CurrentDiscount);
            RaisePropertyChanged(() => ProductCostWithDiscount);
        }
    }

    public string Supplier
    {
        get => Product.Supplier;
        set
        {
            Product.Supplier = value;
            RaisePropertyChanged(() => Supplier);
        }
    }

    public decimal ProductCostWithDiscount =>
        CurrentDiscount == 0 ? ProductCost : ProductCost - ProductCost * (CurrentDiscount / 100.0m);

    public Product Product { get; init; }
}