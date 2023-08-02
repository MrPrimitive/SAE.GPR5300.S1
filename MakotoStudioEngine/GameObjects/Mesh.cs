using MSE.Engine.Core;
using MSE.Engine.Utils;
using Silk.NET.OpenGL;

namespace MSE.Engine.GameObjects {
  public class Mesh : IDisposable {
    public List<Texture> Textures { get; private set; } = new();
    public uint IndicesLength { get; }
    private readonly VertexArrayObject<float, uint> _vertexArrayObject;

    public Mesh(GL gl, float[] vertices, uint[] indices) {
      IndicesLength = (uint)indices.Length;
      var ebo = new VertexBufferObject<uint>(gl, indices, BufferTargetARB.ElementArrayBuffer);
      var vbo = new VertexBufferObject<float>(gl, vertices, BufferTargetARB.ArrayBuffer);
      _vertexArrayObject = new VertexArrayObject<float, uint>(gl, vbo, ebo);
      _vertexArrayObject.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 8, 0);
      _vertexArrayObject.VertexAttributePointer(1, 3, VertexAttribPointerType.Float, 8, 3);
      _vertexArrayObject.VertexAttributePointer(2, 2, VertexAttribPointerType.Float, 8, 6);
    }

    public void Bind() {
      _vertexArrayObject.Bind();
      for (int i = 0; i < Textures.Count; i++) {
        Textures[i].Bind(TextureUnit.Texture0 + i);
      }
    }

    public void BindCubeMap() {
      _vertexArrayObject.Bind();
      Textures[0].BindCubeMap();
    }

    public void Dispose() {
      Textures = null;
      _vertexArrayObject.Dispose();
    }
  }
}