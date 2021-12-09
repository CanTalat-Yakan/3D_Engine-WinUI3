using System.Collections.Generic;
using UWP3DEngine.Assets.Engine.Data;

namespace UWP3DEngine.Assets.Engine.Helper
{
    internal class CObjLoader
    {
        internal static CMeshInfo LoadFilePro(string _fileName)
        {
            var con = new Assimp.AssimpContext();
            var file = con.ImportFile(_fileName);

            CMeshInfo obj = new CMeshInfo();

            obj.Vertices = new List<CVertex>();
            obj.Indices = new List<ushort>();
            foreach (var mesh in file.Meshes)
            {
                for (int i = 0; i < mesh.VertexCount; i++)
                    obj.Vertices.Add(new CVertex(
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
                    obj.Indices.AddRange(rangeIndices);
                    if (item.IndexCount == 4)
                    {
                        rangeIndices = new ushort[] {
                        (ushort)(item.Indices[0]),
                        (ushort)(item.Indices[2]),
                        (ushort)(item.Indices[3])};
                        obj.Indices.AddRange(rangeIndices);
                    }
                }
            }

            return obj;
        }
    }
}
