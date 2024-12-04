using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace Idibri.RevitPlugin.Common.UserControls
{
    public partial class Spinner : UserControl
    {
        #region Dependency Properties
        #region Selected Item
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(Spinner), new PropertyMetadata(null, OnSelectedItemPropertyChanged));

        private static void OnSelectedItemPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as Spinner).UpdateSelectedItemIndex();
        }

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        #endregion

        #region Items Source
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IList), typeof(Spinner), new PropertyMetadata(null, OnItemsSourcePropertyChanged));

        private static void OnItemsSourcePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as Spinner).UpdateSelectedItemIndex();
        }

        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        #endregion
        #endregion

        #region Properties
        public int SelectedItemIndex { get; private set; }
        #endregion

        #region Constructors
        public Spinner()
        {
            InitializeComponent();
            UpdateSelectedItemIndex();
        }
        #endregion

        #region Methods
        private void UpdateSelectedItemIndex()
        {
            if (SelectedItem != null && ItemsSource != null)
            {
                SelectedItemIndex = ItemsSource.IndexOf(SelectedItem);
            }
            else
            {
                SelectedItemIndex = -1;
            }
        }
        #endregion

        #region Event Handlers
        private void IncrementButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (SelectedItemIndex == -1 && ItemsSource.Count > 0)
            {
                SelectedItem = ItemsSource[0];
            }
            else if (SelectedItemIndex >= 0 && SelectedItemIndex < ItemsSource.Count - 1)
            {
                SelectedItem = ItemsSource[SelectedItemIndex + 1];
            }
        }

        private void DecrementButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (SelectedItemIndex == -1 && ItemsSource.Count > 0)
            {
                SelectedItem = ItemsSource[ItemsSource.Count - 1];
            }
            else if (SelectedItemIndex > 0)
            {
                SelectedItem = ItemsSource[SelectedItemIndex - 1];
            }
        }
        #endregion
    }
}
