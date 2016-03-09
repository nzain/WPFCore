using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFCore.Converters
{
    /// <summary>Bool to visibility converter.</summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        private Visibility _trueVisibility = Visibility.Visible;
        private Visibility _falseVisibility = Visibility.Collapsed;

        /// <summary>Gets or sets the visibility value to use for a <c>true</c> value.</summary>
        public Visibility TrueVisibility
        {
            get { return this._trueVisibility; }
            set { this._trueVisibility = value; }
        }

        /// <summary>Gets or sets the visibility value to use for a <c>false</c> value.</summary>
        public Visibility FalseVisibility
        {
            get { return this._falseVisibility; }
            set { this._falseVisibility = value; }
        }

        /// <summary>Converts a bool to visibility.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The optional parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return this.FalseVisibility;
            }
            if (!(value is bool))
            {
                string msg = string.Format("Converter expected type bool, but got type " + value.GetType());
                throw new ArgumentException(msg, "value");
            }
            return ((bool)value)
                ? this.TrueVisibility
                : this.FalseVisibility;
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