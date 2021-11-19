using System;
using System.Linq;
using WinUI3DEngine.Assets.Engine.Components;
using WinUI3DEngine.Assets.Engine.Editor;
using Vector2 = SharpDX.Vector2;
using Vector3 = SharpDX.Vector3;

namespace WinUI3DEngine.Assets.Engine.Utilities
{
    internal class CScene
    {
        public string m_profile;

        internal CCamera m_camera = new CCamera();
        internal CController m_cameraController;
        internal Engine_ObjectManager m_objectManager = new Engine_ObjectManager();

        internal void Awake()
        {
            m_cameraController = new CCameraController(m_camera);

            m_objectManager.CreateSky();
        }

        Engine_Object parent, subParent;
        internal void Start()
        {
            parent = m_objectManager.CreateEmpty("Content");
            subParent = m_objectManager.CreateEmpty("Cubes");
            subParent.m_parent = parent;

            m_objectManager.CreatePrimitive(EPrimitiveTypes.SPHERE, parent).m_transform.m_position = new Vector3(0, 0, 1);
            m_objectManager.CreatePrimitive(EPrimitiveTypes.SPHERE, parent).m_transform.m_position = new Vector3(0, 0, -3);
            m_objectManager.CreatePrimitive(EPrimitiveTypes.SPHERE, parent).m_transform.m_position = new Vector3(0, 2.5f, 0);
            m_objectManager.CreatePrimitive(EPrimitiveTypes.SPHERE, parent).m_transform.m_position = new Vector3(0, -4, 0);
            m_objectManager.CreatePrimitive(EPrimitiveTypes.SPHERE, parent).m_transform.m_position = new Vector3(2, 0, 0);
            m_objectManager.CreatePrimitive(EPrimitiveTypes.SPHERE, parent).m_transform.m_position = new Vector3(-1, 1, 0);
            m_objectManager.CreatePrimitive(EPrimitiveTypes.CUBE, subParent);
        }

        internal void Update()
        {
            m_camera.RecreateViewConstants();
            m_cameraController.Update();

            if (CInput.Instance.GetKey(Windows.System.VirtualKey.C, CInput.Input_State.DOWN))
                m_objectManager.CreatePrimitive(EPrimitiveTypes.CUBE, subParent).m_transform = new CTransform
                {
                    m_rotation = new Vector3(new Random().Next(1, 360), new Random().Next(1, 360), new Random().Next(1, 360)),
                    m_scale = new Vector3(new Random().Next(1, 3), new Random().Next(1, 3), new Random().Next(1, 3))
                };
        }

        internal void LateUpdate()
        {
            m_profile = "Objects: " + m_objectManager.m_list.Count().ToString();

            int vertexCount = 0;
            foreach (var item in m_objectManager.m_list)
                if (item.m_enabled && item.m_mesh != null)
                    vertexCount += item.m_mesh.m_vertexCount;
            m_profile += "\n" + "Vertices: " + vertexCount;
        }

        internal void Render()
        {
            foreach (var item in m_objectManager.m_list)
                if (item.m_enabled && item.m_mesh != null)
                    item.Update_Render();

            m_objectManager.m_sky.Update_Render();
        }
    }
}
