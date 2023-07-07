using System.Numerics;
using MSE.Engine.Core;
using Silk.NET.OpenGL;

namespace MSE.Engine.GameObjects {
  public class Mesh : IDisposable {
    public GL Gl { get; }
    public Vector3[] Vertices { get; }
    public Vector3[] Normals { get; }
    public Vector2[] Uvs { get; }
    public uint[] Indices { get; }
    public List<Texture> Textures { get; private set; } = new();
    public VertexArrayObject VertexArrayObject { get; set; }

    public Mesh(GL gl, Vector3[] vertices, Vector3[] normals, Vector2[] uvs, uint[] indices) {
      Gl = gl;
      Vertices = vertices;
      Normals = normals;
      Uvs = uvs;
      Indices = indices;
    }

    public void Dispose() {
      Textures = null;
      VertexArrayObject.Dispose();
    }
  }
}