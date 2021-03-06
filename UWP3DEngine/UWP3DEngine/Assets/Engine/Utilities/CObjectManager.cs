using System;
using System.Collections.Generic;
using UWP3DEngine.Assets.Engine.Components;
using UWP3DEngine.Assets.Engine.Helper;
using Vector3 = SharpDX.Vector3;

namespace UWP3DEngine.Assets.Engine.Utilities
{
    public enum EPrimitiveTypes
    {
        CUBE,
        SPHERE,
        PLANE,
        CYLINDER,
        CAPSULE
    }
    class MyList<T> : List<T>
    {
        public event EventHandler OnAdd;
        public new void Add(T item) // "new" to avoid compiler-warnings, because we're hiding a method from base-class
        {
            if (null != OnAdd)
                OnAdd(this, null);
            base.Add(item);
        }
    }
    internal class CObjectManager
    {
        internal MyList<CObject> m_List = new MyList<CObject>();
        internal CObject m_Sky;

        CMaterial m_materialDefault;
        CMaterial m_materialReflection;
        CMaterial m_materialSky;
        CMesh m_meshSphere;
        CMesh m_meshCube;

        static readonly string SHADER_LIT = @"Assets//Engine//Resources//Shader//Lit.hlsl";
        static readonly string SHADER_SIMPLELIT = @"Assets//Engine//Resources//Shader//SimpleLit.hlsl";
        static readonly string SHADER_UNLIT = @"Assets//Engine//Resources//Shader//Unlit.hlsl";
        static readonly string IMAGE_DEFAULT = @"Assets//Engine//Resources//Textures//Default.png";
        static readonly string IMAGE_SKY = @"Assets//Engine//Resources//Textures//Sky3.jpg";
        static readonly string OBJ_CUBE = @"Assets//Engine//Resources//Models//Cube.obj";
        static readonly string OBJ_SPHERE = @"Assets//Engine//Resources//Models//Sphere.obj";

        internal CObjectManager()
        {
            m_materialDefault = new CMaterial(SHADER_SIMPLELIT, IMAGE_DEFAULT);
            m_materialReflection = new CMaterial(SHADER_LIT, IMAGE_SKY);
            m_materialSky = new CMaterial(SHADER_UNLIT, IMAGE_SKY);

            m_meshCube = new CMesh(CObjLoader.LoadFilePro(OBJ_CUBE));
            m_meshSphere = new CMesh(CObjLoader.LoadFilePro(OBJ_SPHERE));
        }


        internal CObject Duplicate(CObject _refObject)
        {
            CObject gObject = _refObject.Clone();

            m_List.Add(gObject);
            return gObject;
        }

        internal CObject CreateEmpty(string _name = "Entity")
        {
            CObject gObject = new CObject()
            {
                m_Name = _name,
                m_Material = m_materialDefault,
            };

            m_List.Add(gObject);
            return gObject;
        }

        internal CObject CreatePrimitive(EPrimitiveTypes _type)
        {
            CObject gObject = new CObject();
            gObject.m_Material = m_materialReflection;

            switch (_type)
            {
                case EPrimitiveTypes.CUBE:
                    gObject.m_Mesh = m_meshCube;
                    gObject.m_Name = "Cube" + m_List.Count.ToString();
                    break;
                case EPrimitiveTypes.SPHERE:
                    gObject.m_Mesh = m_meshSphere;
                    gObject.m_Name = "Sphere" + m_List.Count.ToString();
                    break;
                case EPrimitiveTypes.PLANE:
                    break;
                case EPrimitiveTypes.CYLINDER:
                    break;
                case EPrimitiveTypes.CAPSULE:
                    break;
                default:
                    break;
            }

            m_List.Add(gObject);
            return gObject;
        }
        internal CObject CreatePrimitive(EPrimitiveTypes _type, CObject _parent)
        {
            var gObject = CreatePrimitive(_type);
            gObject.m_Parent = _parent;

            return gObject;
        }

        internal void CreateSky()
        {
            m_Sky = new CObject()
            {
                m_Name = "Sky",
                m_Mesh = m_meshSphere,
                m_Material = m_materialSky,
            };

            m_Sky.m_Transform.m_Scale = new Vector3(-1000, -1000, -1000);
        }
    }
}
