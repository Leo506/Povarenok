using System;
using System.Globalization;
using System.Windows.Data;

namespace DemoExam.Wpf.Converters;

public class NumberToDiscountStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var discount = (byte)value;
        return $"Discount {discount}%";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}