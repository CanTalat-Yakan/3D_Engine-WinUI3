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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI3DEngine.UserControls
{
    public sealed partial class ViewPort : UserControl
    {
        internal TextBlock m_DebugProfiling;
        internal Grid m_BorderBrush;

        public ViewPort()
        {
            this.InitializeComponent();

            m_DebugProfiling = x_TextBlock_Debug_FPS;
            m_BorderBrush = x_Grid_ViewPort_BorderBrush;
        }

        void Slider_FOV_ValueChanged(object sender, RangeBaseValueChangedEventArgs e) { }
        void NumberBox_Speed_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args) { }
    }
}
