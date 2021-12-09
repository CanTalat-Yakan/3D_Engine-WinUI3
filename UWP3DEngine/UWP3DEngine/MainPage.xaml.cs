using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UWP3DEngine.Assets.Controls;

namespace UWP3DEngine
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        CMain m_mainControl;

        public MainPage()
        {
            this.InitializeComponent();

            m_mainControl = new CMain(x_Grid_Main, x_TextBlock_Status_Content);
            m_mainControl.m_Player = new CPlayer(x_AppBarToggleButton_Status_Play, x_AppBarToggleButton_Status_Pause, x_AppBarButton_Status_Forward);
        }

        private void AppBarToggleButton_Status_Play_Click(object sender, RoutedEventArgs e) { m_mainControl.m_Player.Play(); }
        private void AppBarToggleButton_Status_Pause_Click(object sender, RoutedEventArgs e) { m_mainControl.m_Player.Pause(); }
        private void AppBarButton_Status_Forward_Click(object sender, RoutedEventArgs e) { m_mainControl.m_Player.Forward(); }
        private void AppBarButton_Status_Kill_Click(object sender, RoutedEventArgs e) { m_mainControl.m_Player.Kill(); }
        private void AppBarToggleButton_Status_Light(object sender, RoutedEventArgs e) { x_Frame_Main.RequestedTheme = x_Frame_Main.RequestedTheme == ElementTheme.Light ? ElementTheme.Dark : x_Frame_Main.RequestedTheme = ElementTheme.Light; }
    }
}
