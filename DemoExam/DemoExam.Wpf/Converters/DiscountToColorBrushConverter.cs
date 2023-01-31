using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DemoExam.Wpf.Converters;

public class DiscountToColorBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var discount = (byte)value;

        if (discount > 15)
        {
            var color = (Color)ColorConverter.ConvertFromString("#7fff00");
            return new SolidColorBrush(color);
        }

        return Color.FromRgb(1, 1, 1);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}