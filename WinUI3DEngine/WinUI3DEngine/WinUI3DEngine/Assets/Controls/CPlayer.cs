using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUI3DEngine.Assets.Controls
{
    internal enum EPlayMode
    {
        NONE,
        PLAYING,
        PAUSED
    }
    internal class CPlayer
    {
        internal EPlayMode m_playMode;

        AppBarToggleButton m_play;
        AppBarToggleButton m_pause;
        AppBarButton m_forward;
        TextBlock m_status;
        COutput m_output;

        internal CPlayer(AppBarToggleButton play, AppBarToggleButton pause, AppBarButton forward)
        {
            m_play = play;
            m_pause = pause;
            m_forward = forward;
            m_output = CMain.Singleton.m_Layout.m_Output.m_Control;
            m_status = CMain.Singleton.m_Status;
        }

        void SetStatusAppBarButtons(bool _b)
        {
            m_playMode = _b ? EPlayMode.PLAYING : EPlayMode.NONE;

            m_pause.IsEnabled = _b;
            m_forward.IsEnabled = _b;
            m_pause.IsChecked = false;

            m_play.Label = _b ? "Stop" : "Play";
            m_play.Icon = _b ? new SymbolIcon(Symbol.Stop) : new SymbolIcon(Symbol.Play);
        }
        void SetStatus(string _s)
        {
            m_status.Text = _s;
        }

        internal void Play()
        {
            if (m_playMode == EPlayMode.NONE)
                if (m_output.m_ClearPlay.IsChecked.Value)
                    m_output.ClearOutput();

            CMain.Singleton.m_Layout.m_ViewPort.m_BorderBrush.BorderBrush = new SolidColorBrush(Colors.GreenYellow);
            CMain.Singleton.m_Layout.m_ViewPort.m_BorderBrush.BorderThickness = new Thickness(m_play.IsChecked.Value ? 2 : 0);

            SetStatusAppBarButtons(m_play.IsChecked.Value);

            SetStatus(m_play.IsChecked.Value ? "Now Playing..." : "Stopped Gamemode");
        }
        internal void Pause()
        {
            m_playMode = m_pause.IsChecked.Value ? EPlayMode.PAUSED : EPlayMode.PLAYING;

            CMain.Singleton.m_Layout.m_ViewPort.m_BorderBrush.BorderBrush = new SolidColorBrush(m_pause.IsChecked.Value ? Colors.Orange : Colors.GreenYellow);

            SetStatus(m_pause.IsChecked.Value ? "Paused Gamemode" : "Continued Gamemode");
        }
        internal void Forward()
        {
            if (m_playMode != EPlayMode.PAUSED)
                return;

            COutput.Log("Stepped Forward..");

            SetStatus("Stepped Forward");
        }
        internal void Kill()
        {
            m_play.IsChecked = false;

            SetStatusAppBarButtons(false);

            SetStatus("Killed Process of GameInstance!");
        }
    }
}
