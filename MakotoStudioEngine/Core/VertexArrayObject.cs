using MakotoStudioEngine.GameObjects;
using MakotoStudioEngine.Interfaces;
using Silk.NET.OpenGL;

namespace MakotoStudioEngine.Core {
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