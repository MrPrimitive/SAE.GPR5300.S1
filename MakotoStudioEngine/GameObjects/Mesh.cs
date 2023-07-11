using MSE.Engine.Core;
using MSE.Engine.Utils;
using Silk.NET.OpenGL;

namespace MSE.Engine.GameObjects {
  public class Mesh : IDisposable {
    public float[] Vertices { get; }
    public uint[] Indices { get; }
    public List<Texture> Textures { get; private set; } = new();
    public VertexArrayObjectOld<float, uint> VertexArrayObject { get; }
    public uint IndicesLength { get; }
    private BufferObject<uint> Ebo;
    private BufferObject<float> Vbo;

    public Mesh(float[] vertices, uint[] indices) {
      Vertices = vertices;
      Indices = indices;
      IndicesLength = (uint)Indices.Length;
    }

    public Mesh(GL gl, float[] vertices, uint[] indices) {
      Vertices = vertices;
      Indices = indices;
      IndicesLength = (uint)Indices.Length;
      Ebo = new BufferObject<uint>(gl, Indices, BufferTargetARB.ElementArrayBuffer);
      Vbo = new BufferObject<float>(gl, Vertices, BufferTargetARB.ArrayBuffer);
      VertexArrayObject = new VertexArrayObjectOld<float, uint>(gl, Vbo, Ebo);
      VertexArrayObject.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 8, 0);
      VertexArrayObject.VertexAttributePointer(1, 3, VertexAttribPointerType.Float, 8, 3);
      VertexArrayObject.VertexAttributePointer(2, 2, VertexAttribPointerType.Float, 8, 6);
    }

    public void Bind() {
      VertexArrayObject.Bind();
      for (int i = 0; i < Textures.Count; i++) {
        Textures[i].Bind(TextureUnit.Texture0 + i);
      }
    }

    public void Dispose() {
      Textures = null;
      VertexArrayObject.Dispose();
    }
  }
}