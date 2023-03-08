using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DemoExam.Wpf.Converters;

public class NumberToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            var number = (double)value;
            return number == 0 ? Visibility.Hidden : Visibility.Visible;
        }
        catch (Exception e)
        {
            return Visibility.Visible;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}