using System;
using System.Windows.Data;
using System.Globalization;

namespace APLPX.UI.WPF.Converters
{

    public class ModeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                string mode = parameter.ToString();

                if (mode.Equals("Manual"))
                    return true;
            }

            return false;


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    
}
