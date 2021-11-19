using Vector2 = SharpDX.Vector2;
using Vector3 = SharpDX.Vector3;

namespace WinUI3DEngine.Assets.Engine.Data
{
    struct CVertex
    {
        public Vector3 pos;
        public Vector2 texCoord;
        public Vector3 normal;

        public CVertex(
            float x, float y, float z,
            float u, float v,
            float nx, float ny, float nz)
        {
            pos = new Vector3(x, y, z);
            texCoord = new Vector2(u, v);
            normal = new Vector3(nx, ny, nz);
        }
        public CVertex(
            Vector3 _pos,
            Vector2 _tex,
            Vector3 _nor)
        {
            pos = _pos;
            texCoord = _tex;
            normal = _nor;
        }
    }
}
