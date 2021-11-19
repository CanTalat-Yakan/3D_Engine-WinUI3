using System;
using System.Collections.Generic;
using WinUI3DEngine.Assets.Engine.Components;
using WinUI3DEngine.Assets.Engine.Helper;
using Vector3 = SharpDX.Vector3;

namespace WinUI3DEngine.Assets.Engine.Utilities
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
    internal class Engine_ObjectManager
    {
        internal MyList<Engine_Object> m_list = new MyList<Engine_Object>();
        internal Engine_Object m_sky;

        CMaterial m_materialDefault;
        CMaterial m_materialReflection;
        CMaterial m_materialSky;
        CMesh m_meshSphere;
        CMesh m_meshCube;

        static readonly string SHADER_LIT = @"Assets//Engine//Resources//Shader//Lit.hlsl";
        static readonly string SHADER_SIMPLELIT = @"Assets//Engine//Resources//Shader//SimpleLit.hlsl";
        static readonly string SHADER_UNLIT = @"Assets//Engine//Resources//Shader//Unlit.hlsl";
        static readonly string IMAGE_DEFAULT = @"Assets//Engine//Resources//Textures//Default.png";
        static readonly string IMAGE_SKY = @"Assets//Engine//Resources//Texture//Sky4.jpg";
        static readonly string OBJ_CUBE = @"Assets//Engine//Resources//Models//Cube.obj";
        static readonly string OBJ_SPHERE = @"Assets//Engine//Resources//Models//Sphere.obj";

        internal Engine_ObjectManager()
        {
            m_materialDefault = new CMaterial(SHADER_SIMPLELIT, IMAGE_DEFAULT);
            m_materialReflection = new CMaterial(SHADER_LIT, IMAGE_SKY);
            m_materialSky = new CMaterial(SHADER_UNLIT, IMAGE_SKY);

            m_meshCube = new CMesh(CObjLoader.LoadFilePro(OBJ_CUBE));
            m_meshSphere = new CMesh(CObjLoader.LoadFilePro(OBJ_SPHERE));
        }


        internal Engine_Object Duplicate(Engine_Object _refObject)
        {
            Engine_Object gObject = _refObject.Clone();

            m_list.Add(gObject);
            return gObject;
        }

        internal Engine_Object CreateEmpty(string _name = "Entity")
        {
            Engine_Object gObject = new Engine_Object()
            {
                m_name = _name,
                m_material = m_materialDefault,
            };

            m_list.Add(gObject);
            return gObject;
        }

        internal Engine_Object CreatePrimitive(EPrimitiveTypes _type)
        {
            Engine_Object gObject = new Engine_Object();
            gObject.m_material = m_materialReflection;

            switch (_type)
            {
                case EPrimitiveTypes.CUBE:
                    gObject.m_mesh = m_meshCube;
                    gObject.m_name = "Cube" + m_list.Count.ToString();
                    break;
                case EPrimitiveTypes.SPHERE:
                    gObject.m_mesh = m_meshSphere;
                    gObject.m_name = "Sphere" + m_list.Count.ToString();
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

            m_list.Add(gObject);
            return gObject;
        }
        internal Engine_Object CreatePrimitive(EPrimitiveTypes _type, Engine_Object _parent)
        {
            var gObject = CreatePrimitive(_type);
            gObject.m_parent = _parent;

            return gObject;
        }

        internal void CreateSky()
        {
            m_sky = new Engine_Object()
            {
                m_name = "Sky",
                m_mesh = m_meshSphere,
                m_material = m_materialSky,
            };

            m_sky.m_transform.m_scale = new Vector3(-1000, -1000, -1000);
        }
    }
}
