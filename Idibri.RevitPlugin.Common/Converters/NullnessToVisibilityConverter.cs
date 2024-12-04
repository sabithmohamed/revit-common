using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Idibri.RevitPlugin.Common.Converters
{
    public class NullnessToVisibilityConverter : IValueConverter
    {
        public bool Reversed
        {
            get { return _reversed; }
            set { _reversed = value; }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            if (Reversed)
            {
                return value != null ? Visibility.Collapsed : Visibility.Visible;
            }
            else
            {
                return value == null ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public object ConvertBack(object target, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }

        private bool _reversed = false;
    }
}
