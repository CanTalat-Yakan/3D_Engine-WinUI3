using System;
using UWP3DEngine.Assets.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace UWP3DEngine.UserControls
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
