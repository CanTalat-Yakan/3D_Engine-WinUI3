using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using UWP3DEngine.Assets.Engine.Utilities;
using UWP3DEngine.UserControls;

namespace UWP3DEngine.Assets.Controls
{
    class TreeEntry
    {
        public Guid ID;
        public Guid? IDparent;
        public string Name;
        public CObject Object;
        public TreeViewNode Node;
    }
    class TreeEntryCollection
    {
        public List<TreeEntry> m_Hierarchy = new List<TreeEntry>();

        public string[] ToStringArray()
        {
            string[] s = new string[m_Hierarchy.Count];

            for (int i = 0; i < m_Hierarchy.Count; i++)
                s[i] = GetParents(m_Hierarchy[i], m_Hierarchy[i].Name, '/');

            return s;
        }

        string GetParents(TreeEntry _current, string _path, char _pathSeperator)
        {
            if (_current.IDparent != null)
                foreach (var item in m_Hierarchy)
                    if (item.ID == _current.IDparent)
                        _path = GetParents(
                            item,
                            item.Name + _pathSeperator + _path,
                            _pathSeperator);

            return _path;
        }
    }
    class CHierarchy
    {
        internal TreeView m_tree;
        internal TreeEntryCollection m_collection;
        internal MyList<CObject> m_engineObjectsList;

        public CHierarchy(TreeView _tree)
        {
            m_tree = _tree;

            m_engineObjectsList = CMain.Singleton.m_Layout.m_ViewPort.m_Engine.m_Scene.m_ObjectManager.m_List;
            m_engineObjectsList.OnAdd += (object sender, EventArgs e) =>
            {
                m_tree.RootNodes.Clear();
                Initialize();
            };

            Initialize();

            m_tree.ItemInvoked += (TreeView sender, TreeViewItemInvokedEventArgs e) =>
            {
                COutput.Log(e.InvokedItem.ToString());
                CMain.Singleton.m_Layout.m_PropertiesHolder.Children.Clear();
                CMain.Singleton.m_Layout.m_PropertiesHolder.Children.Add(new Properties());
            };
        }

        void Initialize()
        {
            m_collection = new TreeEntryCollection();
            foreach (var item in m_engineObjectsList)
                m_collection.m_Hierarchy.Add(
                    new TreeEntry()
                    {
                        Object = item,
                        Name = item.m_Name,
                        ID = item.ID,
                        Node = new TreeViewNode()
                        {
                            Content = item.m_Name,
                            IsExpanded = true,
                        }
                    });

            foreach (var entry in m_collection.m_Hierarchy)
                if (entry.Object.m_Parent != null)
                    entry.IDparent = entry.Object.m_Parent.ID;

            List<TreeViewNode> hierarchy = new List<TreeViewNode>();
            foreach (var entry in m_collection.m_Hierarchy)
                if (entry.IDparent is null)
                    hierarchy.Add(RecursiveGetChildren(m_collection, entry).Node);

            foreach (var entry in hierarchy)
                m_tree.RootNodes.Add(entry);
        }

        TreeEntry RecursiveGetChildren(TreeEntryCollection _collection, TreeEntry _input)
        {
            foreach (var item in _collection.m_Hierarchy)
                if (item.IDparent == _input.ID)
                    _input.Node.Children.Add(RecursiveGetChildren(_collection, item).Node);

            return _input;
        }
    }
}
