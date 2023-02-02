using System;
using System.Globalization;
using System.Windows.Data;

namespace DemoExam.Wpf.Converters;

public class DecimalToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var decimalValue = (decimal)value;
        if (decimalValue == 0)
            return string.Empty;
        return decimalValue.ToString(CultureInfo.InvariantCulture);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}