using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WPFCore.Converters
{
    [ValueConversion(typeof(int), typeof(bool))]
    public class ProgressToIndeterminateConverter : IValueConverter
    {
        private static readonly ProgressToIndeterminateConverter Singleton = new ProgressToIndeterminateConverter();

        public static ProgressToIndeterminateConverter Instance { get { return Singleton; } }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return true;
            }
            return (int)value == 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
