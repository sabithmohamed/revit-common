using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Idibri.RevitPlugin.Common
{
    public class ImperialLength : IComparable<ImperialLength>
    {
        public int Feet { get; private set; }
        public double Inches { get; private set; }

        static Regex FeetAndInchesRegex = new Regex(@"^(?:(\d+)')?\s*(?:(?:(?:(\d+)?(?: (\d+)/(\d+))?)|(\d+)/(\d+)|(\d*\.\d+))"")?$");
        
        public ImperialLength(int feet, double inches)
        {
            Feet = feet;
            Inches = inches;
        }

        public static ImperialLength TryParse(string str)
        {
            Match match = FeetAndInchesRegex.Match(str ?? "");

            if (match.Success)
            {
                int feet = 0;
                double inches = 0;

                feet = match.Groups[1].Success ? int.Parse(match.Groups[1].Value) : 0;
                inches = match.Groups[2].Success ? int.Parse(match.Groups[2].Value) : 0;

                if (match.Groups[3].Success || match.Groups[5].Success)
                {
                    int inchesPartNumerator = 0;
                    int inchesPartDenominator = 0;

                    if (match.Groups[3].Success)
                    {
                        inchesPartNumerator = int.Parse(match.Groups[3].Value);
                        inchesPartDenominator = int.Parse(match.Groups[4].Value);
                    }
                    else if (match.Groups[5].Success)
                    {
                        inchesPartNumerator = int.Parse(match.Groups[5].Value);
                        inchesPartDenominator = int.Parse(match.Groups[6].Value);
                    }

                    if (inchesPartDenominator == 0) { return null; }

                    inches += (double)inchesPartNumerator / inchesPartDenominator;
                }
                else if (match.Groups[7].Success)
                {
                    inches = double.Parse(match.Groups[7].Value);
                }

                return new ImperialLength(feet, inches);
            }
            return null;
        }

        public static string DoubleToFraction(double value, IEnumerable<int> denominators, double closeness)
        {
            foreach (int denominator in denominators)
            {
                double v = value * denominator;
                int w = (int)Math.Round(v);

                if (Math.Abs(v - w) <= closeness)
                {
                    if (denominator == 1)
                    {
                        return w.ToString();
                    }
                    else if (w > denominator)
                    {
                        int whole = w / denominator;
                        int numerator = w - (whole * denominator);
                        return string.Format("{0} {1}/{2}", whole, numerator, denominator);
                    }
                    else
                    {
                        return string.Format("{0}/{1}", w, denominator);
                    }
                }
            }

            return null;
        }

        public override string ToString()
        {
            return string.Format("{0}'{1}\"", Feet, Inches);
        }

        public string ToString(IEnumerable<int> denominators, double closeness)
        {
            return ToString(denominators, closeness, null);
        }

        public string ToString(IEnumerable<int> denominators, double closeness, string inchesFormatString)
        {
            StringBuilder sb = new StringBuilder("");

            if (Feet != 0)
            {
                sb.Append(Feet);
                sb.Append('\'');

                if (Inches != 0)
                {
                    sb.Append(' ');
                }
            }

            if (Inches != 0)
            {
                string fraction = DoubleToFraction(Inches, denominators, closeness);
                if (fraction != null)
                {
                    sb.Append(fraction);
                }
                else
                {
                    sb.Append(Inches.ToString(inchesFormatString));
                }
                sb.Append('"');
            }

            return sb.ToString();
        }

        public int CompareTo(ImperialLength o)
        {
            if (Feet < o.Feet)
            {
                return -1;
            }
            else if (Feet > o.Feet)
            {
                return 1;
            }
            else
            {
                if (Inches < o.Inches)
                {
                    return -1;
                }
                else if (Inches > o.Inches)
                {
                    return 1;
                }
                return 0;
            }
        }
    }
}
