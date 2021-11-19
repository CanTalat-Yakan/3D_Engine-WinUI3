using SharpDX;
using WinUI3DEngine.Assets.Engine.Data;

namespace WinUI3DEngine.Assets.Engine.Components
{
    internal class CTransform
    {
        internal CTransform m_Parent;

        internal SPerModelConstantBuffer m_ConstantsBuffer { get => new SPerModelConstantBuffer() { ModelView = m_WorldMatrix }; }
        internal Matrix m_WorldMatrix = Matrix.Identity;
        internal Matrix m_NormalMatrix = Matrix.Identity;
        internal Vector3 m_Position = Vector3.Zero;
        internal Vector3 m_Rotation = Vector3.Zero;
        internal Vector3 m_Scale = Vector3.One;

        internal void Udate()
        {
            Matrix translation = Matrix.Translation(m_Position);
            Matrix rotation = Matrix.RotationYawPitchRoll(m_Rotation.X, m_Rotation.Y, m_Rotation.Z);
            Matrix scale = Matrix.Scaling(m_Scale);

            m_WorldMatrix = Matrix.Transpose(scale * rotation * translation);
            if (m_Parent != null) m_WorldMatrix = m_WorldMatrix * m_Parent.m_WorldMatrix;
        }

        public override string ToString()
        {
            string s = "";
            s += m_Position.ToString() + "\n";
            s += m_Rotation.ToString() + "\n";
            s += m_Scale.ToString();
            return s;
        }
    }
}
