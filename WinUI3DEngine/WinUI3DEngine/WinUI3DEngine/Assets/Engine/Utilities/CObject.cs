using System;
using WinUI3DEngine.Assets.Engine.Components;

namespace WinUI3DEngine.Assets.Engine.Utilities
{
    internal class Engine_Object : ICloneable
    {
        internal Guid ID = Guid.NewGuid();

        internal Engine_Object m_parent;

        internal CTransform m_transform = new CTransform();
        internal CMaterial m_material;
        internal CMesh m_mesh;

        internal string m_name = "Object";
        internal bool m_enabled = true;
        internal bool m_static = false;

        internal Engine_Object Clone() { return (Engine_Object)this.MemberwiseClone(); }
        object ICloneable.Clone() { return Clone(); }

        internal void Update_Render()
        {
            if (!m_static)
            {
                if (m_parent != null)
                    m_transform.m_parent = m_parent.m_transform;
                m_transform.Udate();
            }
            m_material.Render(m_transform.m_constantsBuffer);
            m_mesh.Render();
        }
    }
}
