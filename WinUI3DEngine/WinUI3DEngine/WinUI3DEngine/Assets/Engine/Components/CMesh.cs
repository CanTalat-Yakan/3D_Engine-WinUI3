using System;
using System.Linq;
using WinUI3DEngine.Assets.Engine.Data;
using WinUI3DEngine.Assets.Engine.Utilities;
using D3D11 = SharpDX.Direct3D11;

namespace WinUI3DEngine.Assets.Engine.Components
{
    internal class CMesh
    {
        CRenderer m_d3d;

        internal D3D11.Buffer m_VertexBuffer;
        internal D3D11.Buffer m_IndexBuffer;

        internal int m_VertexCount;
        internal int m_VertexStride;
        internal int m_IndexCount;
        internal int m_IndexStride;


        internal CMesh(CMeshInfo _obj)
        {
            #region //Get Instance of DirectX
            m_d3d = CRenderer.Instance;
            #endregion

            #region //Set Variables
            m_VertexCount = _obj.Vertices.Count();
            m_VertexStride = SharpDX.Utilities.SizeOf<CVertex>();

            m_IndexCount = _obj.Indices.Count();
            m_IndexStride = sizeof(int);
            #endregion

            #region //Create VertexBuffer
            unsafe
            {
                fixed (CVertex* p = _obj.Vertices.ToArray()) //Array
                {
                    IntPtr ptr = (IntPtr)p;
                    m_VertexBuffer = new D3D11.Buffer(
                      m_d3d.m_Device,
                      ptr,
                      new D3D11.BufferDescription(
                          m_VertexCount * m_VertexStride, //ByteWidth
                          D3D11.BindFlags.VertexBuffer,
                          D3D11.ResourceUsage.Default));
                }
            }
            #endregion

            #region //Create IndexBuffer
            unsafe
            {
                fixed (ushort* p = _obj.Indices.ToArray())
                {
                    IntPtr ptr = (IntPtr)p;
                    m_IndexBuffer = new D3D11.Buffer(
                      m_d3d.m_Device,
                      ptr,
                      new D3D11.BufferDescription(
                          m_IndexCount * m_IndexStride,
                          D3D11.BindFlags.IndexBuffer,
                          D3D11.ResourceUsage.Default));
                }
            }
            #endregion
        }

        internal void Render()
        {
            m_d3d.RenderMesh(
                m_VertexBuffer, m_VertexStride,
                m_IndexBuffer, m_IndexCount);
        }
    }
}
