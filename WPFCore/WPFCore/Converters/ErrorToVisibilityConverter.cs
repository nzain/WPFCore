using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace WPFCore.Converters
{
    /// <summary>Error to visibility converter.</summary>
    [ValueConversion(typeof(string), typeof(Visibility))]
    public class ErrorToVisibilityConverter : IValueConverter
    {
        private Visibility _nullOrEmptyVisibility = Visibility.Collapsed;
        private Visibility _nonEmptyVisibility = Visibility.Visible;

        /// <summary>Gets or sets the Visibility to use for null or empty strings.</summary>
        public Visibility NullOrEmptyVisibility
        {
            get { return this._nullOrEmptyVisibility; }
            set { this._nullOrEmptyVisibility = value; }
        }

        /// <summary>Gets or sets the visibility to use for non-empty strings.</summary>
        public Visibility NonEmptyVisibility
        {
            get { return this._nonEmptyVisibility; }
            set { this._nonEmptyVisibility = value; }
        }

        /// <summary>Converts an error to visibility.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The optional parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.ToString().Length == 0)
            {
                return this.NullOrEmptyVisibility;
            }
            return this.NonEmptyVisibility;
        }

        /// <summary>Not supported.</summary>
        /// <param name="value">The value to convert back.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The optional parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The converted value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}