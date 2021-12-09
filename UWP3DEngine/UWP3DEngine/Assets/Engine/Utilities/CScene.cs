using System;
using System.Linq;
using UWP3DEngine.Assets.Engine.Components;
using UWP3DEngine.Assets.Engine.Editor;
using Vector2 = SharpDX.Vector2;
using Vector3 = SharpDX.Vector3;

namespace UWP3DEngine.Assets.Engine.Utilities
{
    internal class CScene
    {
        internal string m_Profile;

        internal CCamera m_Camera = new CCamera();
        internal CController m_CameraController;
        internal CObjectManager m_ObjectManager = new CObjectManager();

        internal void Awake()
        {
            m_CameraController = new CController(m_Camera);

            m_ObjectManager.CreateSky();
        }

        CObject subParent;
        internal void Start()
        {
            CObject parent = m_ObjectManager.CreateEmpty("Content");
            subParent = m_ObjectManager.CreateEmpty("Cubes");
            subParent.m_Parent = parent;

            m_ObjectManager.CreatePrimitive(EPrimitiveTypes.SPHERE, parent).m_Transform.m_Position = new Vector3(0, 0, 1);
            m_ObjectManager.CreatePrimitive(EPrimitiveTypes.SPHERE, parent).m_Transform.m_Position = new Vector3(0, 0, -3);
            m_ObjectManager.CreatePrimitive(EPrimitiveTypes.SPHERE, parent).m_Transform.m_Position = new Vector3(0, 2.5f, 0);
            m_ObjectManager.CreatePrimitive(EPrimitiveTypes.SPHERE, parent).m_Transform.m_Position = new Vector3(0, -4, 0);
            m_ObjectManager.CreatePrimitive(EPrimitiveTypes.SPHERE, parent).m_Transform.m_Position = new Vector3(2, 0, 0);
            m_ObjectManager.CreatePrimitive(EPrimitiveTypes.SPHERE, parent).m_Transform.m_Position = new Vector3(-1, 1, 0);
            m_ObjectManager.CreatePrimitive(EPrimitiveTypes.CUBE, subParent);
        }

        internal void Update()
        {
            m_Camera.RecreateViewConstants();
            m_CameraController.Update();

            if (CInput.Instance.GetKey(Windows.System.VirtualKey.C, CInput.EInputState.DOWN))
                m_ObjectManager.CreatePrimitive(EPrimitiveTypes.CUBE, subParent).m_Transform = new CTransform
                {
                    m_Rotation = new Vector3(new Random().Next(1, 360), new Random().Next(1, 360), new Random().Next(1, 360)),
                    m_Scale = new Vector3(new Random().Next(1, 3), new Random().Next(1, 3), new Random().Next(1, 3))
                };
        }

        internal void LateUpdate()
        {
            m_Profile = "Objects: " + m_ObjectManager.m_List.Count().ToString();

            int vertexCount = 0;
            foreach (var item in m_ObjectManager.m_List)
                if (item.m_Enabled && item.m_Mesh != null)
                    vertexCount += item.m_Mesh.m_VertexCount;
            m_Profile += "\n" + "Vertices: " + vertexCount;
        }

        internal void Render()
        {
            foreach (var item in m_ObjectManager.m_List)
                if (item.m_Enabled && item.m_Mesh != null)
                    item.Update_Render();

            m_ObjectManager.m_Sky.Update_Render();
        }
    }
}
