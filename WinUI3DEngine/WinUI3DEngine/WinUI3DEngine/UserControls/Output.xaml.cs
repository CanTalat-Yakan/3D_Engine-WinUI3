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
    public sealed partial class Output : UserControl
    {
        public Output()
        {
            this.InitializeComponent();
        }


        private void AppBarButton_Output_Clear(object sender, RoutedEventArgs e) { } // m_control.ClearOutput(); }
        private void AppBarToggleButton_Output_Collapse_Click(object sender, RoutedEventArgs e) { } // Control_Output.IterateOutputMessages(); }
        private void AppBarToggleButton_Filter_Click(object sender, RoutedEventArgs e) { } // Control_Output.IterateOutputMessages(); }
        private void AppBarToggleButton_Debug_ErrorPause_Click(object sender, RoutedEventArgs e) { }
        private void AppBarToggleButton_Debug_ClearPlay_Click(object sender, RoutedEventArgs e) { }
    }
}
