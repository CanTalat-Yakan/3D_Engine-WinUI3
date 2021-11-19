using SharpDX.Mathematics.Interop;
using Vector2 = SharpDX.Vector2;
using Vector3 = SharpDX.Vector3;
using Vector4 = SharpDX.Vector4;

namespace WinUI3DEngine.Assets.Engine.Data
{
    public struct SViewConstantsBuffer
    {
        public RawMatrix ViewProjection;
        public RawMatrix View;
        public RawMatrix Projection;
        public RawVector3 WorldCamPos;
        public float pad;
    }

    public struct SPerModelConstantBuffer
    {
        public RawMatrix ModelView;
    }

    public struct SDirectionalLightConstantBuffer
    {
        public Vector3 direction;
        public float pad;
        public Vector4 diffuse;
        public Vector4 ambient;
        public float intensity;
        public Vector3 pad2;
    }

    public struct SPointLightConstantBuffer
    {
        public Vector3 position;
        public float pad;
        public Vector4 diffuse;
        public float intensity;
        public float radius;
        public Vector2 pad2;
    }
}
