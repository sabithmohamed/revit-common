using System.Windows;
using System.Windows.Controls;

namespace Idibri.RevitPlugin.Common.UserControls
{
    // I pulled this code from http://sladapter.blogspot.com/2010/11/how-to-bind-to-silverlight-treeview.html
    /// <summary>
    /// A tree view that allows you to set and get the Selected Item.
    /// </summary>
    public class ExtendedTreeView : TreeView
    {
        public ExtendedTreeView()
            : base()
        {
            this.SelectedItemChanged += TreeViewEx_SelectedItemChanged;
        }

        void TreeViewEx_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.SelectedItem = e.NewValue;
        }

        #region SelectedItem

        /// <summary>
        /// Gets or Sets the SelectedItem possible Value of the TreeViewItem object.
        /// </summary>
        public new object SelectedItem
        {
            get { return this.GetValue(ExtendedTreeView.SelectedItemProperty); }
            set
            {
                if (value == null)
                {
                    TreeViewItem tvi = FindItemNode(SelectedItem);
                    if (tvi != null)
                    {
                        tvi.IsSelected = false;
                    }
                }

                this.SetValue(ExtendedTreeView.SelectedItemProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public new static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(ExtendedTreeView), new PropertyMetadata(SelectedItemProperty_Changed));

        static void SelectedItemProperty_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            ExtendedTreeView targetObject = dependencyObject as ExtendedTreeView;

            if (targetObject != null && targetObject.SelectedItem != null)
            {
                TreeViewItem tvi = targetObject.FindItemNode(targetObject.SelectedItem) as TreeViewItem;

                if (tvi != null)
                {
                    tvi.IsSelected = true;
                }
            }
        }
        #endregion SelectedItem

        public void ExpandNodeForItem(object item)
        {
            TreeViewItem tvi = FindItemNode(item);

            if (tvi != null)
            {
                tvi.IsExpanded = true;
            }
        }

        public TreeViewItem FindItemNode(object item)
        {
            TreeViewItem node = null;
            foreach (object data in this.Items)
            {
                node = this.ItemContainerGenerator.ContainerFromItem(data) as TreeViewItem;

                if (node != null)
                {
                    if (data == item) { break; }
                    node = FindItemNodeInChildren(node, item);
                    if (node != null) { break; }
                }
            }
            return node;
        }

        protected TreeViewItem FindItemNodeInChildren(TreeViewItem parent, object item)
        {
            if (parent == null) { return null; }
            TreeViewItem node = null;
            bool isExpanded = parent.IsExpanded;
            if (!isExpanded) //Can't find child container unless the parent node is Expanded once
            {
                parent.IsExpanded = true;
                parent.UpdateLayout();
            }
            foreach (object data in parent.Items)
            {
                node = parent.ItemContainerGenerator.ContainerFromItem(data) as TreeViewItem;
                if (data == item && node != null) { break; }
                node = FindItemNodeInChildren(node, item);
                if (node != null) { break; }
            }
            if (node == null && parent.IsExpanded != isExpanded) { parent.IsExpanded = isExpanded; }
            if (node != null) { parent.IsExpanded = true; }
            return node;
        }
    }
}