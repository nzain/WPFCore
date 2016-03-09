using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFCore.Converters
{
    /// <summary>An object to visibility converter.</summary>
    [ValueConversion(typeof(object), typeof(Visibility))]
    public class NullToVisibilityConverter : IValueConverter
    {
        private Visibility _nullVisibility = Visibility.Collapsed;
        private Visibility _notNullVisibility = Visibility.Visible;

        /// <summary>Gets or sets the Visibility for a <c>null</c> value.</summary>
        public Visibility NullVisibility
        {
            get { return this._nullVisibility; }
            set { this._nullVisibility = value; }
        }

        /// <summary>Gets or sets the Visibility for a non-null value.</summary>
        public Visibility NotNullVisibility
        {
            get { return this._notNullVisibility; }
            set { this._notNullVisibility = value; }
        }

        /// <summary>Converts an object to visibility.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The optional parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? this.NullVisibility : this.NotNullVisibility;
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