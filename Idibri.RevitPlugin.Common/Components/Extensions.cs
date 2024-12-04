using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Idibri.RevitPlugin.Common.Extensions
{
    public class TextBoxBehavior
    {
        public static readonly DependencyProperty SelectAllOnFocusProperty = DependencyProperty.RegisterAttached(
            "SelectAllOnFocus",
            typeof(bool),
            typeof(TextBoxBehavior),
            new PropertyMetadata(false, OnSelectAllOnFocusPropertyChanged));

        private static void OnSelectAllOnFocusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = d as TextBox;

            if (textBox == null) { return; }

            bool oldValue = (bool)e.OldValue;
            bool newValue = (bool)e.NewValue;

            if (oldValue == newValue) { return; }

            if (newValue)
            {
                SelectAllOnFocusMethods.SetUp(textBox);
            }
            else
            {
                SelectAllOnFocusMethods.TearDown(textBox);
            }
        }

        public static bool GetSelectAllOnFocus(DependencyObject d)
        {
            return (bool)d.GetValue(SelectAllOnFocusProperty);
        }

        public static void SetSelectAllOnFocus(DependencyObject d, bool value)
        {
            d.SetValue(SelectAllOnFocusProperty, value);
        }

        private class SelectAllOnFocusMethods
        {
            public static void SetUp(TextBox textBox)
            {
                textBox.GotFocus += OnGotFocus;
            }

            public static void TearDown(TextBox textBox)
            {
                textBox.GotFocus -= OnGotFocus;
            }

            private static void OnGotFocus(object sender, RoutedEventArgs e)
            {
                (sender as TextBox).SelectAll();
            }
        }

        public static readonly DependencyProperty UpdateTextBindingOnReturnProperty = DependencyProperty.RegisterAttached(
            "UpdateTextBindingOnReturn",
            typeof(bool),
            typeof(TextBoxBehavior),
            new PropertyMetadata(false, OnUpdateTextBindingOnReturnPropertyChanged));

        private static void OnUpdateTextBindingOnReturnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = d as TextBox;

            if (textBox == null) { return; }

            bool oldValue = (bool)e.OldValue;
            bool newValue = (bool)e.NewValue;

            if (oldValue == newValue) { return; }

            if (newValue)
            {
                UpdateTextBindingOnReturnMethods.SetUp(textBox);
            }
            else
            {
                UpdateTextBindingOnReturnMethods.TearDown(textBox);
            }
        }

        public static bool GetUpdateTextBindingOnReturn(DependencyObject d)
        {
            return (bool)d.GetValue(UpdateTextBindingOnReturnProperty);
        }

        public static void SetUpdateTextBindingOnReturn(DependencyObject d, bool value)
        {
            d.SetValue(UpdateTextBindingOnReturnProperty, value);
        }

        private class UpdateTextBindingOnReturnMethods
        {
            public static void SetUp(TextBox textBox)
            {
                textBox.KeyDown += OnKeyDown;
            }

            public static void TearDown(TextBox textBox)
            {
                textBox.KeyDown -= OnKeyDown;
            }

            private static void OnKeyDown(object sender, KeyEventArgs e)
            {
                switch (e.Key)
                {
                    case Key.Enter:
                        System.Diagnostics.Debug.WriteLine("Updating binding on enter.");
                        Helpers.UpdateBindingExpressionSource(sender as TextBox, TextBox.TextProperty);
                        e.Handled = true;
                        break;
                }
            }
        }
    }
}
