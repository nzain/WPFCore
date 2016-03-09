using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WPFCore.Converters
{
    /// <summary>Enum match parameter to bool converter.</summary>
    public class EnumMatchToBooleanConverter : IValueConverter
    {
        /// <summary>Converts an enum value to <c>true</c> when it matches the given parameter.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The optional parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return false;
            }

            string checkValue = value.ToString();
            string targetValue = parameter.ToString();
            return checkValue.Equals(targetValue,
                StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>Converts the given bool to the enum parameter.</summary>
        /// <param name="value">The value to convert back.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The optional parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The converted value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return null;
            }

            var useValue = (bool)value;
            string targetValue = parameter.ToString();
            if (useValue)
            {
                return Enum.Parse(targetType, targetValue);
            }

            return null;
        }
    }

}