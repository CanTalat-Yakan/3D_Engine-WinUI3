using ImGuiNET;
using System;
using System.Numerics;
using UWP3DEngine.Assets.Engine.Utilities;

namespace UWP3DEngine.Assets.Engine.Editor
{
    internal class CImGui
    {
        CRenderer m_d3d;

        internal CImGui()
        {
            m_d3d = CRenderer.Instance;
            var con = ImGui.CreateContext();
            ImGui.SetCurrentContext(con);
            var fonts = ImGui.GetIO().Fonts;
            ImGui.GetIO().Fonts.AddFontDefault();
            var io = ImGui.GetIO();
            io.DisplaySize = m_d3d.m_SwapChainPanel.ActualSize;
            io.DisplayFramebufferScale = Vector2.One;
            io.DeltaTime = (float)CTime.m_Watch.Elapsed.TotalSeconds;
            ImGui.StyleColorsDark();
            RecreateFontDeviceTexture();
        }

        static void RecreateFontDeviceTexture()
        {
            ImGuiIOPtr io = ImGui.GetIO();
            IntPtr pixels;
            int width, height, bytesPerPixel;
            io.Fonts.GetTexDataAsRGBA32(out pixels, out width, out height, out bytesPerPixel);
            io.Fonts.ClearTexData();
        }

        internal void Draw()
        {
            ImGui.GetIO().DeltaTime = (float)CTime.m_Watch.Elapsed.TotalSeconds;
            ImGui.NewFrame();
            ImGui.Begin("Test");

            ImGuiLayout();

            ImGui.End();
            ImGui.Render();
            unsafe { RenderDrawData(ImGui.GetDrawData()); }
        }

        float f = 0.0f;
        bool show_test_window = false;
        bool show_another_window = false;
        Vector3 clear_color = new Vector3(114f / 255f, 144f / 255f, 154f / 255f);
        byte[] _textBuffer = new byte[100];
        void ImGuiLayout()
        {
            {
                ImGui.Text("Hello, world!");
                ImGui.SliderFloat("float", ref f, 0.0f, 1.0f, string.Empty);
                ImGui.ColorEdit3("clear color", ref clear_color);
                if (ImGui.Button("Test Window")) show_test_window = !show_test_window;
                if (ImGui.Button("Another Window")) show_another_window = !show_another_window;
                ImGui.Text(string.Format("Application average {0:F3} ms/frame ({1:F1} FPS)", 1000f / ImGui.GetIO().Framerate, ImGui.GetIO().Framerate));

                ImGui.InputText("Text input", _textBuffer, 100);
            }

            // 2. Show another simple window, this time using an explicit Begin/End pair
            if (show_another_window)
            {
                ImGui.SetNextWindowSize(new Vector2(200, 100), ImGuiCond.FirstUseEver);
                ImGui.Begin("Another Window", ref show_another_window);
                ImGui.Text("Hello");
                ImGui.End();
            }

            // 3. Show the ImGui test window. Most of the sample code is in ImGui.ShowTestWindow()
            if (show_test_window)
            {
                ImGui.SetNextWindowPos(new Vector2(650, 20), ImGuiCond.FirstUseEver);
                ImGui.ShowDemoWindow(ref show_test_window);
            }
        }

        void RenderDrawData(ImDrawDataPtr drawData)
        {
            // Handle cases of screen coordinates != from framebuffer coordinates (e.g. retina displays)
            drawData.ScaleClipRects(ImGui.GetIO().DisplayFramebufferScale);

            UpdateBuffers(drawData);
            RenderCommandLists(drawData);
        }

        private unsafe void UpdateBuffers(ImDrawDataPtr drawData)
        {
            if (drawData.TotalVtxCount == 0)
                return;
            //// Expand buffers if we need more room
            //if (drawData.TotalVtxCount > _vertexBufferSize)
            //{
            //    _vertexBuffer?.Dispose();

            //    _vertexBufferSize = (int)(drawData.TotalVtxCount * 1.5f);
            //    _vertexBuffer = new VertexBuffer(_graphicsDevice, DrawVertDeclaration.Declaration, _vertexBufferSize, BufferUsage.None);
            //    _vertexData = new byte[_vertexBufferSize * DrawVertDeclaration.Size];
            //}

            //if (drawData.TotalIdxCount > _indexBufferSize)
            //{
            //    _indexBuffer?.Dispose();

            //    _indexBufferSize = (int)(drawData.TotalIdxCount * 1.5f);
            //    _indexBuffer = new IndexBuffer(_graphicsDevice, IndexElementSize.SixteenBits, _indexBufferSize, BufferUsage.None);
            //    _indexData = new byte[_indexBufferSize * sizeof(ushort)];
            //}

            //// Copy ImGui's vertices and indices to a set of managed byte arrays
            //int vtxOffset = 0;
            //int idxOffset = 0;

            //for (int n = 0; n < drawData.CmdListsCount; n++)
            //{
            //    ImDrawListPtr cmdList = drawData.CmdListsRange[n];

            //    fixed (void* vtxDstPtr = &_vertexData[vtxOffset * DrawVertDeclaration.Size])
            //    fixed (void* idxDstPtr = &_indexData[idxOffset * sizeof(ushort)])
            //    {
            //        Buffer.MemoryCopy((void*)cmdList.VtxBuffer.Data, vtxDstPtr, _vertexData.Length, cmdList.VtxBuffer.Size * DrawVertDeclaration.Size);
            //        Buffer.MemoryCopy((void*)cmdList.IdxBuffer.Data, idxDstPtr, _indexData.Length, cmdList.IdxBuffer.Size * sizeof(ushort));
            //    }

            //    vtxOffset += cmdList.VtxBuffer.Size;
            //    idxOffset += cmdList.IdxBuffer.Size;
            //}

            //// Copy the managed byte arrays to the gpu vertex- and index buffers
            //_vertexBuffer.SetData(_vertexData, 0, drawData.TotalVtxCount * DrawVertDeclaration.Size);
            //_indexBuffer.SetData(_indexData, 0, drawData.TotalIdxCount * sizeof(ushort));
        }

        private unsafe void RenderCommandLists(ImDrawDataPtr drawData)
        {
            //            _graphicsDevice.SetVertexBuffer(_vertexBuffer);
            //            _graphicsDevice.Indices = _indexBuffer;

            //            int vtxOffset = 0;
            //            int idxOffset = 0;

            //            for (int n = 0; n < drawData.CmdListsCount; n++)
            //            {
            //                ImDrawListPtr cmdList = drawData.CmdListsRange[n];

            //                for (int cmdi = 0; cmdi < cmdList.CmdBuffer.Size; cmdi++)
            //                {
            //                    ImDrawCmdPtr drawCmd = cmdList.CmdBuffer[cmdi];

            //                    if (!_loadedTextures.ContainsKey(drawCmd.TextureId))
            //                    {
            //                        throw new InvalidOperationException($"Could not find a texture with id '{drawCmd.TextureId}', please check your bindings");
            //                    }

            //                    _graphicsDevice.ScissorRectangle = new Rectangle(
            //                        (int)drawCmd.ClipRect.X,
            //                        (int)drawCmd.ClipRect.Y,
            //                        (int)(drawCmd.ClipRect.Z - drawCmd.ClipRect.X),
            //                        (int)(drawCmd.ClipRect.W - drawCmd.ClipRect.Y)
            //                    );

            //                    var effect = UpdateEffect(_loadedTextures[drawCmd.TextureId]);

            //                    foreach (var pass in effect.CurrentTechnique.Passes)
            //                    {
            //                        pass.Apply();

            //#pragma warning disable CS0618 // // FNA does not expose an alternative method.
            //                        _graphicsDevice.DrawIndexedPrimitives(
            //                            primitiveType: PrimitiveType.TriangleList,
            //                            baseVertex: vtxOffset,
            //                            minVertexIndex: 0,
            //                            numVertices: cmdList.VtxBuffer.Size,
            //                            startIndex: idxOffset,
            //                            primitiveCount: (int)drawCmd.ElemCount / 3
            //                        );
            //#pragma warning restore CS0618
            //                    }

            //                    idxOffset += (int)drawCmd.ElemCount;
            //                }

            //                vtxOffset += cmdList.VtxBuffer.Size;
            //            }
        }
    }
}
