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

namespace WinUI3DEngine.UserControls
{
    public sealed partial class Output : UserControl
    {
        internal COutput m_Control;

        public Output()
        {
            this.InitializeComponent();

            m_Control = new COutput(
                x_Stackpanel_Output, 
                x_ScrollViewer_Output, 
                x_AppBarToggleButton_Output_Collapse, 
                x_AppBarToggleButton_Filter_Messages, 
                x_AppBarToggleButton_Filter_Warnings, 
                x_AppBarToggleButton_Filter_Errors, 
                x_AppBarToggleButton_Debug_ErrorPause, 
                x_AppBarToggleButton_Debug_ClearPlay);

            DispatcherTimer dispatcher = new DispatcherTimer();
            dispatcher.Interval = TimeSpan.FromMilliseconds(42);
            dispatcher.Tick += Tick;
            //dispatcher.Start();
            DispatcherTimer dispatcherSec = new DispatcherTimer();
            dispatcherSec.Interval = TimeSpan.FromSeconds(1);
            dispatcherSec.Tick += TickSec;
            dispatcherSec.Start();
        }

        void Tick(object sender, object e)
        {
            if (CMain.Singleton.m_Player.m_PlayMode == EPlayMode.PLAYING)
                Update();
        }
        void TickSec(object sender, object e)
        {
            if (CMain.Singleton.m_Player.m_PlayMode == EPlayMode.PLAYING)
                UpdateSec();
        }

        void Update()
        {
            COutput.Log("Updated Frame..");
        }
        void UpdateSec()
        {
            ExampleSkriptDebugTest();
        }

        void ExampleSkriptDebugTest()
        {
            Random rnd = new Random();
            int i = rnd.Next(0, 24);

            COutput.Log(i.ToString());
            if (i < 5)
                COutput.Log("Error Example!", EMessageType.ERROR);
            if (i < 10 && i > 5)
                COutput.Log("A Warning.", EMessageType.WARNING);
            if (i < 15)
                COutput.Log("This is a Message");
            if (i > 15)
                Test();
        }
        void Test()
        {
            COutput.Log("Test");
        }

        private void AppBarButton_Output_Clear(object sender, RoutedEventArgs e) { m_Control.ClearOutput(); }
        private void AppBarToggleButton_Output_Collapse_Click(object sender, RoutedEventArgs e) { COutput.IterateOutputMessages(); }
        private void AppBarToggleButton_Filter_Click(object sender, RoutedEventArgs e) { COutput.IterateOutputMessages(); }
        private void AppBarToggleButton_Debug_ErrorPause_Click(object sender, RoutedEventArgs e) { }
        private void AppBarToggleButton_Debug_ClearPlay_Click(object sender, RoutedEventArgs e) { }
    }
}
