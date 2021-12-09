using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using UWP3DEngine.Assets.Engine.Editor;
using UWP3DEngine.Assets.Engine.Utilities;

namespace UWP3DEngine.Assets.Engine
{
    internal class CEngine
    {
        internal CInput m_Input;
        internal CTime m_Time;
        internal CScene m_Scene;
        internal CRenderer m_Render;
        internal CImGui m_Gui;

        internal CEngine(SwapChainPanel _swapChainPanel, TextBlock _textBlock)
        {
            m_Render = new CRenderer(_swapChainPanel);
            m_Input = new CInput();
            m_Time = new CTime();
            m_Scene = new CScene();
            m_Gui = new CImGui();

            m_Scene.Awake();
            m_Scene.Start();

            CompositionTarget.Rendering += (s, e) =>
            {
                m_Render.Clear();

                m_Input.Update();

                m_Scene.Update();
                m_Scene.LateUpdate();

                m_Input.LateUpdate();

                m_Time.Update();

                m_Render.SetSolid();
                m_Scene.Render();
                m_Render.SetWireframe();
                m_Scene.Render();

                m_Gui.Draw();

                m_Render.Present();

                _textBlock.Text = m_Time.m_Profile;
                _textBlock.Text += "\n\n" + m_Render.m_Profile;
                _textBlock.Text += "\n\n" + m_Scene.m_Profile;
            };
        }
    }
}
