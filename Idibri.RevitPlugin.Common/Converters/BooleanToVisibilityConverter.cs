using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Idibri.RevitPlugin.Common.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public bool Reversed
        {
            get { return _reversed; }
            set { _reversed = value; }
        }
        private bool _reversed = false;

        public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            if (value is bool)
            {
                if ((bool)value)
                {
                    if (Reversed)
                    {
                        return Visibility.Collapsed;
                    }
                    else
                    {
                        return Visibility.Visible;
                    }
                }
                else
                {
                    if (Reversed)
                    {
                        return Visibility.Visible;
                    }
                    else
                    {
                        return Visibility.Collapsed;
                    }
                }
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            if (value is Visibility)
            {
                Visibility visibility = (Visibility)value;
                bool returnValue = false;

                switch (visibility)
                {
                    case Visibility.Collapsed:
                        returnValue = false;
                        break;
                    case Visibility.Visible:
                        returnValue = true;
                        break;
                }

                if (Reversed)
                {
                    return !returnValue;
                }
                else
                {
                    return returnValue;
                }
            }
            return true;
        }
    }
}
