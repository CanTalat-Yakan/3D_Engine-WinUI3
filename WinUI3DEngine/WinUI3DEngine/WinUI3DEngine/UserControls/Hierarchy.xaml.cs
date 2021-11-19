using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinUI3DEngine.Assets.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI3DEngine.UserControls
{
    public sealed partial class Hierarchy : UserControl
    {
        TreeView m_treeView;

        public Hierarchy()
        {
            this.InitializeComponent();
            m_treeView = x_TreeView_Hierarchy;

            CMain.Singleton.m_Content.Loaded += Initialize0;
        }


        void Initialize0(object sender, RoutedEventArgs e)
        {
            CMain.Singleton.m_Layout.m_ViewPort.Loaded += Initialize;
        }

        void Initialize(object sender, RoutedEventArgs e)
        {
            //var engineObjectList = CMain.Singleton.m_Layout.m_ViewPort.m_renderer.m_scene.m_objectManager.m_list;
            //engineObjectList.OnAdd += list_OnAdd;

            //CScene scene = new CScene();
            //foreach (var item in engineObjectList)
            //{
            //    var newEntry = new TreeEntry() { Object = item, Name = item.m_name, ID = item.ID };
            //    if (item.m_parent != null)
            //        newEntry.IDparent = item.m_parent.ID;

            //    scene.m_Hierarchy.Add(newEntry);
            //}

            //foreach (var item in scene.m_Hierarchy)
            //    item.Node = new Microsoft.UI.Xaml.Controls.TreeViewNode() { Content = item.Name, IsExpanded = true };

            //TreeEntry tmp;
            //foreach (var item in scene.m_Hierarchy)
            //    if ((tmp = scene.GetParent(item)) != null)
            //    {
            //        if (!x_TreeView_Hierarchy.RootNodes.Contains(tmp.Node))
            //            x_TreeView_Hierarchy.RootNodes.Add(tmp.Node);

            //        if (!tmp.Node.Children.Contains(item.Node))
            //            foreach (var child in scene.GetChildren(tmp))
            //                tmp.Node.Children.Add(child.Node);
            //    }
            //    else if (!x_TreeView_Hierarchy.RootNodes.Contains(item.Node))
            //        x_TreeView_Hierarchy.RootNodes.Add(item.Node);
        }

        void list_OnAdd(object sender, EventArgs e)
        {
            //var engineObjectList = m_main.m_Layout.m_ViewPort.m_renderer.m_scene.m_objectManager.m_list;

            //CScene scene = new CScene();

            //var newObject = engineObjectList[engineObjectList.Count - 1];
            //var newEntry = new TreeEntry() { Object = newObject, Name = newObject.m_name, ID = newObject.ID };
            //if (newObject.m_parent != null)
            //    newEntry.IDparent = newObject.m_parent.ID;

            //newEntry.Node = new Microsoft.UI.Xaml.Controls.TreeViewNode() { Content = newEntry.Name, IsExpanded = true };
            //scene.m_Hierarchy.Add(newEntry);

            //TreeEntry tmp;
            //if ((tmp = scene.GetParent(newEntry)) != null)
            //{
            //    if (!x_TreeView_Hierarchy.RootNodes.Contains(tmp.Node))
            //        x_TreeView_Hierarchy.RootNodes.Add(tmp.Node);

            //    tmp.Node.Children.Add(newEntry.Node);
            //}
            //else if (!x_TreeView_Hierarchy.RootNodes.Contains(newEntry.Node))
            //    x_TreeView_Hierarchy.RootNodes.Add(newEntry.Node);
        }
    }
}
