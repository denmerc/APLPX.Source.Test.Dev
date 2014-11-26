using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace APLPX.UI.WPF.Converters
{
    /// <summary>
    /// Inverse of built-in BooleanToVisibilityConverter. Converts false to Visibility.Visible and true to Visibility.Collapsed.
    /// </summary>
    public class InverseBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool inputValue = System.Convert.ToBoolean(value);

            Visibility result = (inputValue ? Visibility.Collapsed : Visibility.Visible);

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = false;
            if (value is Visibility)
            {
                Visibility inputValue = (Visibility)value;
                result = (inputValue == Visibility.Collapsed ? true : false);
            }

            return result;
        }
    }
}
