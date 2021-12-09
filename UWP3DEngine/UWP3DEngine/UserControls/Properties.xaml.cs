using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UWP3DEngine.Assets.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWP3DEngine.UserControls
{
    public sealed partial class Properties : UserControl
    {
        internal CProperties m_Control;

        internal event PropertyChangedEventHandler m_PropertyChanged;

        public Properties()
        {
            this.InitializeComponent();

            m_Control = new CProperties();
            List<Grid> collection = new List<Grid>();
            collection.Add(m_Control.CreateColorButton());
            collection.Add(m_Control.CreateNumberInput());
            collection.Add(m_Control.CreateTextInput());
            collection.Add(m_Control.CreateVec2Input());
            collection.Add(m_Control.CreateVec3Input());
            collection.Add(m_Control.CreateSlider());
            collection.Add(m_Control.CreateBool());
            collection.Add(m_Control.CreateTextureSlot());
            collection.Add(m_Control.CreateReferenceSlot());
            collection.Add(m_Control.CreateHeader());
            collection.Add(m_Control.WrapExpander(m_Control.CreateEvent()));
            x_StackPanel_Properties.Children.Add(m_Control.CreateScript("Example", collection.ToArray()));
            x_StackPanel_Properties.Children.Add(m_Control.CreateScript("Another", m_Control.CreateNumberInput()));
        }

        void AppBarButton_Click_SelectImagePath(object sender, RoutedEventArgs e) { }//m_Control.SelectImage(Img_SelectTexture, x_TextBlock_TexturePath); }
        void AppBarButton_Click_SelectFilePath(object sender, RoutedEventArgs e) { }//m_Control.SelectFile(x_TextBlock_FilePath); }
        void FirePropertyChanged([CallerMemberName] string memberName = null) { this.m_PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName)); }
    }
}
