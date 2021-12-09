using System;
using UWP3DEngine.Assets.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using TreeView = Microsoft.UI.Xaml.Controls.TreeView;
using TreeViewNode = Microsoft.UI.Xaml.Controls.TreeViewNode;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace UWP3DEngine.UserControls
{
    public sealed partial class Hierarchy : UserControl
    {
        TreeView m_treeView;

        public Hierarchy()
        {
            this.InitializeComponent();
            m_treeView = x_TreeView_Hierarchy;

            CMain.Singleton.m_Content.Loaded +=
                (object _sender, RoutedEventArgs _e) =>
                    CMain.Singleton.m_Layout.m_ViewPort.Loaded +=
                        (object sender, RoutedEventArgs e) =>
                            new CHierarchy(x_TreeView_Hierarchy);
        }
    }
}
