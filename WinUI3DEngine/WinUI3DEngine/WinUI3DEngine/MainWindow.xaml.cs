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

namespace WinUI3DEngine
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        CMain m_mainControl;

        public MainWindow()
        {
            this.InitializeComponent();

            m_mainControl = new CMain(x_Grid_Main, x_TextBlock_Status_Content);
            m_mainControl.m_Player = new CPlayer(x_AppBarToggleButton_Status_Play, x_AppBarToggleButton_Status_Pause, x_AppBarButton_Status_Forward);
            string s = File.ReadAllText(@"F://Desktop//txt.txt");
            _ = s;
        }

        private void AppBarToggleButton_Status_Play_Click(object sender, RoutedEventArgs e) { m_mainControl.m_Player.Play(); }
        private void AppBarToggleButton_Status_Pause_Click(object sender, RoutedEventArgs e) { m_mainControl.m_Player.Pause(); }
        private void AppBarButton_Status_Forward_Click(object sender, RoutedEventArgs e) { m_mainControl.m_Player.Forward(); }
        private void AppBarButton_Status_Kill_Click(object sender, RoutedEventArgs e) { m_mainControl.m_Player.Kill(); }
        private void AppBarToggleButton_Status_Light(object sender, RoutedEventArgs e) { x_Frame_Main.RequestedTheme = x_Frame_Main.RequestedTheme == ElementTheme.Light ? ElementTheme.Dark : x_Frame_Main.RequestedTheme = ElementTheme.Light; }

    }
}
