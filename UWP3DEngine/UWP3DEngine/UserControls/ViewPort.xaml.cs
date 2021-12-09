using Microsoft.UI.Xaml.Controls;
using UWP3DEngine.Assets.Engine;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace UWP3DEngine.UserControls
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
            Window.Current.CoreWindow.PointerReleased += m_Engine.m_Input.PointerReleased;
            Window.Current.CoreWindow.PointerMoved += m_Engine.m_Input.PointerMoved;
            Window.Current.CoreWindow.KeyDown += m_Engine.m_Input.KeyDown;
            Window.Current.CoreWindow.KeyUp += m_Engine.m_Input.KeyUp;
            //Window.Current.CoreWindow.GetKeyState(Windows.System.VirtualKey.Control).HasFlag(CoreVirtualKeyStates.Down);
        }

        void Slider_FOV_ValueChanged(object sender, RangeBaseValueChangedEventArgs e) { }
        void NumberBox_Speed_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args) { }
    }
}
