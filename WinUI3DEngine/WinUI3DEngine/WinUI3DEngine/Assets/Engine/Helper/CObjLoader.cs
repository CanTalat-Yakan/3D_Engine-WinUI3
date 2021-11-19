using System.Collections.Generic;
using WinUI3DEngine.Assets.Engine.Data;

namespace WinUI3DEngine.Assets.Engine.Helper
{
    internal class CObjLoader
    {
        internal static CMeshInfo LoadFilePro(string _fileName)
        {
            var a = new Assimp.AssimpContext();
            var s = a.ImportFile(_fileName);

            CMeshInfo obj = new CMeshInfo();

            obj.vertices = new List<CVertex>();
            obj.indices = new List<ushort>();
            foreach (var mesh in s.Meshes)
            {
                for (int i = 0; i < mesh.VertexCount; i++)
                    obj.vertices.Add(new CVertex(
                    mesh.Vertices[i].X,
                    mesh.Vertices[i].Y,
                    mesh.Vertices[i].Z,
                    mesh.TextureCoordinateChannels[0][i].X,
                    mesh.TextureCoordinateChannels[0][i].Y,
                    mesh.Normals[i].X,
                    mesh.Normals[i].Y,
                    mesh.Normals[i].Z));

                foreach (var item in mesh.Faces)
                {
                    var rangeIndices = new ushort[] {
                        (ushort)(item.Indices[0]),
                        (ushort)(item.Indices[1]),
                        (ushort)(item.Indices[2])};
                    obj.indices.AddRange(rangeIndices);
                    if (item.IndexCount == 4)
                    {
                        rangeIndices = new ushort[] {
                        (ushort)(item.Indices[0]),
                        (ushort)(item.Indices[2]),
                        (ushort)(item.Indices[3])};
                        obj.indices.AddRange(rangeIndices);
                    }
                }
            }

            return obj;
        }
    }
}
