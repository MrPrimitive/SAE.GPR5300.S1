using MSE.Engine.GameObjects;
using MSE.Engine.Interfaces;
using Silk.NET.OpenGL;

namespace MSE.Engine.Core {
  public class VertexArrayObject : GenericVertexArrayObject {
    public VertexArrayObject(GL gl, Material program, IGenericVertexBufferObject[] vertexBufferObjects)
      : base(gl, program, false) {
      Init(vertexBufferObjects);
    }

    ~VertexArrayObject() {
      Dispose(false);
    }
  }
}