using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MapResourceExplorer.Model;
using System.Collections;

namespace MapResourceExplorer.UI
{
    /// <summary>
    /// Interaction logic for ExplorerForm.xaml
    /// </summary>
    public partial class ExplorerForm : UserControl
    {
        public ExplorerForm()
        {
            InitializeComponent();
        }

        public void ForceRefresh()
        {
            if (this.IsVisible)
            {
                BindTreeView(treeView1);
            }

        }


        private void BindTreeView(TreeView tree)
        {
            tree.Items.Clear();

            ResourceManager resourceMgr = ResourceManager.Instance;

            Dictionary<string, string> resourceTypes = resourceMgr.GetResourceAllTypes();
            foreach (var resType in resourceTypes)
            {
                TreeViewItem resourceTypeitem = new TreeViewItem();
                resourceTypeitem.Header = resType.Key;
                resourceTypeitem.ToolTip = resType.Value;
                //Bind resource to resourceItemType
                Dictionary<string, string> resList = resourceMgr.GetResourcesByType(resType.Key);

                foreach (var item in resList)
                {
                    TreeViewItem resItem = new TreeViewItem();
                    resItem.Header = item.Key;
                    resItem.ToolTip = item.Value;
                    resItem.Tag = "IsResource";

                    //TODO: Add a context numu
                    resourceTypeitem.Items.Add(resItem);


                }

                tree.Items.Add(resourceTypeitem);
            }



        }


        //
        private void TreeViewItem_PreviewMouseRightButtonDown(Object sender, MouseButtonEventArgs e)
        {
            var treeViewItem = VisualUpwardsSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        static DependencyObject VisualUpwardsSearch<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T))
            {
                source = VisualTreeHelper.GetParent(source);
            }

            return source;
        }

        /// <summary>
        /// right click Item to select item. for context menu
        /// 
        /// http://www.cnblogs.com/TianFang/archive/2010/02/10/1667153.html
        /// http://www.cnblogs.com/tianfang/archive/2010/02/10/1667186.html
        /// </summary>
        /// 
        bool isResItemSlected = false;
        private void treeView1_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            object item = GetElementFromPoint((ItemsControl)sender, e.GetPosition((ItemsControl)sender));
            isResItemSlected = (item != null);
        }

        private object GetElementFromPoint(ItemsControl itemsControl, Point point)
        {
            UIElement element = itemsControl.InputHitTest(point) as UIElement;
            while (element != null)
            {
                if (element == itemsControl)
                {
                    return null;
                }

                object item = itemsControl.ItemContainerGenerator.ItemFromContainer(element);
                if (!item.Equals(DependencyProperty.UnsetValue))
                {
                    return item;
                }

                element = (UIElement)VisualTreeHelper.GetParent(element);
            }

            return null;
        }

        private void ShowResourceContent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("ShowResourceConten" + (sender as TreeViewItem).Header);

        }

        private void ShowResourceContent_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isResItemSlected;
        }

        private void MenuItem_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RefreshButton_Clicked(object sender, RoutedEventArgs e)
        {
            ForceRefresh();
        }

        private void ShowResourceContent_Clicked(object sender, RoutedEventArgs e)
        {
            if (isResItemSlected)
            {
                TreeViewItem item = treeView1.SelectedItem as TreeViewItem;
                string resId = item.ToolTip.ToString();
                string resXml = ResourceManager.Instance.GetResourceContent(resId);
                XmlEditor.Instance(resXml).ShowDialog();
            }
        }

        private void ContextMenu_Opening(object sender, ContextMenuEventArgs e)
        {
            if (!isResItemSlected)
            {
                // disable menu item
            }
        }

        private void ShowResourceHeader_Clicked(object sender, RoutedEventArgs e)
        {
            if (isResItemSlected)
            {
                TreeViewItem item = treeView1.SelectedItem as TreeViewItem;
                string resId = item.ToolTip.ToString();
                string resXml = ResourceManager.Instance.GetResourceHeader(resId);
                XmlEditor.Instance(resXml).ShowDialog();
            }
        }
    }
}
