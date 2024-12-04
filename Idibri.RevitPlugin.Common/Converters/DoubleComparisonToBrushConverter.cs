using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Idibri.RevitPlugin.Common.Converters
{
    public enum ComparisonType
    {
        Equal,
        NotEqual,
        LessThan,
        LessThanEqual,
        GreaterThan,
        GreaterThanEqual
    };

    public class DoubleComparisonToBrushConverter : IValueConverter
    {
        public ComparisonType ComparisonType { get; set; }
        public Brush PassBrush { get; set; }
        public Brush FailBrush { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            if (value == null || parameter == null)
            {
                return GetReturnBrush(false);
            }

            double compare;
            double compareTo;
            bool passes = false;

            if (!double.TryParse(value.ToString(), out compare))
            {
                return GetReturnBrush(false);
            }
            if (!double.TryParse(parameter.ToString(), out compareTo))
            {
                return GetReturnBrush(false);
            }

            switch (ComparisonType)
            {
                case ComparisonType.Equal:
                    passes = compare == compareTo;
                    break;
                case ComparisonType.NotEqual:
                    passes = compare != compareTo;
                    break;
                case ComparisonType.LessThan:
                    passes = compare < compareTo;
                    break;
                case ComparisonType.LessThanEqual:
                    passes = compare <= compareTo;
                    break;
                case ComparisonType.GreaterThan:
                    passes = compare > compareTo;
                    break;
                case ComparisonType.GreaterThanEqual:
                    passes = compare >= compareTo;
                    break;
            }

            return GetReturnBrush(passes);
        }

        private Brush GetReturnBrush(bool passed)
        {
            return passed ? PassBrush : FailBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}
