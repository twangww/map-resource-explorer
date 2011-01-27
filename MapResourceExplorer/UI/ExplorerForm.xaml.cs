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
            //TODO: Refresh all the resources

            BindTreeView(treeView1);
        }


        private void BindTreeView(TreeView tree)
        {
            tree.Items.Clear();

            ResourceManager resourceMgr = ResourceManager.Instance;

            Dictionary<string,string> resourceTypes = resourceMgr.GetResourceAllTypes();
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
                    resourceTypeitem.Items.Add(resItem);
                }

                tree.Items.Add(resourceTypeitem);
            }
          
        }



    }
}
