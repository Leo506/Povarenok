using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;
using DemoExam.Domain.Models;

namespace DemoExam.Wpf.Converters;

public class ProductsAmountInStockToColorBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var order = (Order)value;

        if (order.OrderLists.Any(x => x.Product.ProductQuantityInStock == 0))
        {
            var color = (Color)ColorConverter.ConvertFromString("#ff8c00");
            return new SolidColorBrush(color);
        }

        if (order.OrderLists.All(x => x.Product.ProductQuantityInStock > 3))
        {
            var color = (Color)ColorConverter.ConvertFromString("#20b2aa");
            return new SolidColorBrush(color);
        }

        return Color.FromRgb(1, 1, 1);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}