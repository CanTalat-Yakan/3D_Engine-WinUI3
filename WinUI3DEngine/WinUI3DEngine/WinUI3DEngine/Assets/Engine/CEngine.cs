using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using WinUI3DEngine.Assets.Engine.Editor;
using WinUI3DEngine.Assets.Engine.Utilities;

namespace WinUI3DEngine.Assets.Engine
{
    internal class CEngine
    {
        internal CInput m_input;
        internal CTime m_time;
        internal CScene m_scene;
        internal CRenderer m_render;
        internal CImGui m_gui;

        internal CEngine(SwapChainPanel _swapChainPanel, TextBlock _textBlock)
        {
            m_render = new CRenderer(_swapChainPanel);
            m_input = new CInput();
            m_time = new CTime();
            m_scene = new CScene();
            m_gui = new CImGui();

            m_scene.Awake();
            m_scene.Start();

            CompositionTarget.Rendering += (s, e) =>
            {
                m_render.Clear();

                m_input.Update();

                m_scene.Update();
                m_scene.LateUpdate();

                m_input.LateUpdate();

                m_time.Update();

                m_render.SetSolid();
                m_scene.Render();
                m_render.SetWireframe();
                m_scene.Render();

                m_gui.Draw();

                m_render.Present();

                _textBlock.Text = m_time.m_profile;
                _textBlock.Text += "\n\n" + m_render.m_profile;
                _textBlock.Text += "\n\n" + m_scene.m_profile;
            };
        }
    }
}
