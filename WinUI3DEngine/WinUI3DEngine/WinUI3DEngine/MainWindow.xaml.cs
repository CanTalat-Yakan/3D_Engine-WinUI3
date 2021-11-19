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

            m_mainControl = new CMain();
            m_mainControl.m_Content = x_Grid_Main;
            m_mainControl.Initialize();
        }

        private void AppBarToggleButton_Status_Play_Click(object sender, RoutedEventArgs e) { } //m_GameMode.Play(); }
        private void AppBarToggleButton_Status_Pause_Click(object sender, RoutedEventArgs e) { } //m_GameMode.Pause(); }
        private void AppBarButton_Status_Forward_Click(object sender, RoutedEventArgs e) { } //m_GameMode.Forward(); }
        private void AppBarButton_Status_Kill_Click(object sender, RoutedEventArgs e) { } //m_GameMode.Kill(); }
        private void AppBarToggleButton_Status_Light(object sender, RoutedEventArgs e) { } //RequestedTheme = RequestedTheme == ElementTheme.Light ? ElementTheme.Dark : RequestedTheme = ElementTheme.Light; }

    }
}
