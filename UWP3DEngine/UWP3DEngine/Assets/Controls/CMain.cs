using UWP3DEngine.UserControls;
using Windows.UI.Xaml.Controls;

namespace UWP3DEngine.Assets.Controls
{
    internal class CMain
    {
        public static CMain Singleton { get; private set; }

        internal CLayout m_Layout;
        internal CPlayer m_Player;
        internal Grid m_Content;
        internal TextBlock m_Status;

        public CMain(Grid _content, TextBlock _status)
        {
            if (Singleton is null)
                Singleton = this;

            m_Content = _content;
            m_Status = _status;

            m_Layout = new CLayout(
                m_Content,
                new ViewPort(),
                new Hierarchy(),
                new Properties(),
                new Output(),
                new Files(),
                new Settings());

            m_Layout.Initialize();
        }
    }
}
