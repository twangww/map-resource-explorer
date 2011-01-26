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

        internal void ForceRefresh()
        {
            //TODO: Refresh all the resources

            BindTreeView(treeView1);
        }


        private void BindTreeView(TreeView tree)
        {
            tree.Items.Clear();

            ResourceManager resourceMgr = ResourceManager.Instance;

             ArrayList resourceTypes = resourceMgr.GetResourceAllTypes();
            for (int i = 0; i < resourceTypes.Count; i++)
            {
                TreeViewItem resourceTypeitem = new TreeViewItem();
                resourceTypeitem.Header = resourceTypes[i].ToString();
                resourceTypeitem.ToolTip = resourceTypes[i].ToString();
                //TODO: bind resource to resourceItemType
                //  resourceTypeitem.Items.Add()

                tree.Items.Add(resourceTypeitem);
                
            }
        }



    }
}
