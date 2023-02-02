using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DemoExam.Wpf.Converters;

public class DiscountToTextDecorationConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var discount = (byte)value;
        return discount > 0 ? TextDecorations.Strikethrough : null!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}