using MSE.Engine.Core;

namespace MSE.Engine.GameObjects {
  public class Mesh : IDisposable {
    public float[] Vertices { get; }
    public uint[] Indices { get; }
    public List<Texture> Textures { get; private set; } = new();
    public VertexArrayObjectOld<float, uint> VertexArrayObject { get; set; }
    public uint IndicesLength { get; }

    public Mesh(float[] vertices, uint[] indices) {
      Vertices = vertices;
      Indices = indices;
      IndicesLength = (uint) Indices.Length;
    }

    public void Dispose() {
      Textures = null;
      VertexArrayObject.Dispose();
    }
  }
}