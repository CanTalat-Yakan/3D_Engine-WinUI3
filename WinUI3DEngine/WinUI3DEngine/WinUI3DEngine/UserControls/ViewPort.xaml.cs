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
using WinUI3DEngine.Assets.Engine;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI3DEngine.UserControls
{
    public sealed partial class ViewPort : UserControl
    {
        internal CEngine m_Engine;

        internal TextBlock m_DebugProfiling;
        internal Grid m_BorderBrush;

        public ViewPort()
        {
            this.InitializeComponent();

            m_DebugProfiling = x_TextBlock_Debug_FPS;
            m_BorderBrush = x_Grid_ViewPort_BorderBrush;

            Loaded += Initialize;
        }

        void Initialize(object sender, RoutedEventArgs e)
        {
            m_Engine = new CEngine(x_SwapChainPanel_ViewPort, m_DebugProfiling);

            PointerPressed += m_Engine.m_Input.PointerPressed;
            PointerWheelChanged += m_Engine.m_Input.PointerWheelChanged;
            PointerReleased += m_Engine.m_Input.PointerReleased;
            PointerMoved += m_Engine.m_Input.PointerMoved;
            Window.Current.CoreWindow.KeyDown += m_Engine.m_Input.KeyDown;
            Window.Current.CoreWindow.KeyUp += m_Engine.m_Input.KeyUp;
            //Window.Current.CoreWindow.GetKeyState(Windows.System.VirtualKey.Control).HasFlag(CoreVirtualKeyStates.Down);
        }

        void Slider_FOV_ValueChanged(object sender, RangeBaseValueChangedEventArgs e) { }
        void NumberBox_Speed_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args) { }
    }
}
