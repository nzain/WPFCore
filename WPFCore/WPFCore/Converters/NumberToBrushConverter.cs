using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFCore.Converters
{
    public class NumberToBrushConverter : IValueConverter
    {
        private Brush _lowBrush = Brushes.Black;
        private Brush _mediumBrush = Brushes.DarkOrange;
        private Brush _highBrush = Brushes.Red;

        /// <summary>Gets or sets the Low.</summary>
        public double Low { get; set; }

        /// <summary>Gets or sets the High.</summary>
        public double High { get; set; }

        /// <summary>Gets or sets the LowBrush.</summary>
        public Brush LowBrush
        {
            get { return this._lowBrush; }
            set { this._lowBrush = value; }
        }

        /// <summary>Gets or sets the MediumBrush.</summary>
        public Brush MediumBrush
        {
            get { return this._mediumBrush; }
            set { this._mediumBrush = value; }
        }

        /// <summary>Gets or sets the HighBrush.</summary>
        public Brush HighBrush
        {
            get { return this._highBrush; }
            set { this._highBrush = value; }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (this.Low > this.High)
            {
                throw new InvalidOperationException("Low > High");
            }
            if (value == null)
            {
                return null;
            }
            double d; // we convert the object somehow into this double field - OMG!
            IConvertible convert = value as IConvertible;
            if (convert != null)
            {
                d = convert.ToDouble(null);
            }
            else
            {
                d = double.Parse(value.ToString()); // this will throw, if we don't get something useful...
            }
            if (d <= this.Low)
            {
                return this.LowBrush;
            }
            if (d >= this.High)
            {
                return this.HighBrush;
            }
            return this.MediumBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
