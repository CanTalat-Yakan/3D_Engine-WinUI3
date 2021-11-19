﻿using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinUI3DEngine.UserControls;

namespace WinUI3DEngine.Assets.Controls
{
    internal class CMain
    {
        public CMain Singleton { get; private set; }

        internal CLayout m_Layout;
        internal Grid m_Content;

        public CMain()
        {
            if (Singleton is null)
                Singleton = this;
        }

        internal void Initialize()
        {
            m_Layout = new CLayout(
                m_Content,
                new ViewPort(), 
                new Hierarchy(), 
                new Properties(), 
                new Output(), 
                new Files(), 
                new Settings());

            m_Layout.Initialize();
        }
    }
}