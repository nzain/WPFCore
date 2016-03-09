using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFCore.Converters
{
    [ValueConversion(typeof(object), typeof(Visibility))]
    public class IntToVisibilityConverter : IValueConverter
    {
        private Visibility _targetVisibility = Visibility.Visible;
        private Visibility _nonTargetVisibility = Visibility.Collapsed;

        /// <summary>Gets or sets the TargetValue where the converter returns the <c>TargetVisibility</c>.</summary>
        public int TargetValue { get; set; }

        /// <summary>Gets or sets the Visibility returned when the bound value equals <c>TargetValue</c> (default: Visible).</summary>
        public Visibility TargetVisibility
        {
            get { return this._targetVisibility; }
            set { this._targetVisibility = value; }
        }

        /// <summary>Gets or sets the Visibility returned when the bound value does not equal <c>TargetValue</c> (default:
        /// Collapsed).</summary>
        public Visibility NonTargetVisibility
        {
            get { return this._nonTargetVisibility; }
            set { this._nonTargetVisibility = value; }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int n = (int)value;
            return n == this.TargetValue
                ? this.TargetVisibility
                : this.NonTargetVisibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}