using SharpDX;
using System;
using WinUI3DEngine.Assets.Engine.Data;
using WinUI3DEngine.Assets.Engine.Utilities;
using D3D11 = SharpDX.Direct3D11;

namespace WinUI3DEngine.Assets.Engine.Components
{
    internal class CCamera
    {
        CRenderer m_d3d;

        internal static double m_FOV;
        internal SViewConstantsBuffer m_ViewConstants;
        D3D11.Buffer m_view;

        internal CTransform m_Transform = new CTransform();
        internal Vector3 m_Front = Vector3.ForwardLH;
        internal Vector3 m_Right = Vector3.Right;
        internal Vector3 m_Up = Vector3.Up;
        internal Vector3 m_LocalUp = Vector3.Up;

        internal CCamera()
        {
            #region //Get Instances
            m_d3d = CRenderer.Instance;
            #endregion

            SViewConstantsBuffer cbView = new SViewConstantsBuffer();
            m_view = D3D11.Buffer.Create(m_d3d.m_Device, D3D11.BindFlags.ConstantBuffer, ref cbView);

            RecreateViewConstants();
        }

        internal void RecreateViewConstants()
        {
            #region //Set Transform
            m_Transform.m_Rotation.X %= 360;
            m_Transform.m_Rotation.Y = Math.Clamp(m_Transform.m_Rotation.Y, -89, 89);

            var front = new Vector3(
                MathF.Cos(MathUtil.DegreesToRadians(m_Transform.m_Rotation.X)) * MathF.Cos(MathUtil.DegreesToRadians(m_Transform.m_Rotation.Y)),
                MathF.Sin(MathUtil.DegreesToRadians(m_Transform.m_Rotation.Y)),
                MathF.Sin(MathUtil.DegreesToRadians(m_Transform.m_Rotation.X)) * MathF.Cos(MathUtil.DegreesToRadians(m_Transform.m_Rotation.Y)));
            m_Front = Vector3.Normalize(front);
            m_Right = Vector3.Normalize(Vector3.Cross(m_Front, m_Up));
            m_LocalUp = Vector3.Normalize(Vector3.Cross(m_Right, m_Front));
            #endregion

            #region //Set ViewConstantBuffer
            var view = Matrix.LookAtLH(
                m_Transform.m_Position,
                m_Transform.m_Position + m_Front,
                m_Up);

            var aspect = m_d3d.m_SwapChainPanel.ActualWidth / m_d3d.m_SwapChainPanel.ActualHeight;
            var projection = Matrix.PerspectiveFovLH(
                //MathUtil.DegreesToRadians(2 * (float)Math.Atan(Math.Tan(FOV * 0.5f) * aspect)),
                MathUtil.DegreesToRadians((float)(m_FOV / aspect)),
                (float)aspect,
                0.1f, 1000);

            var viewProjection = Matrix.Transpose(view * projection);
            m_ViewConstants = new SViewConstantsBuffer() { ViewProjection = viewProjection, View = view, Projection = projection, WorldCamPos = m_Transform.m_Position };
            #endregion

            m_d3d.m_DeviceContext.UpdateSubresource(ref m_ViewConstants, m_view);
            m_d3d.m_DeviceContext.VertexShader.SetConstantBuffer(0, m_view);
        }
    }
}
