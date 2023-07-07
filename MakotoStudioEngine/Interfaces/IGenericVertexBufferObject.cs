using Silk.NET.OpenGL;

namespace MSE.Engine.Interfaces {
  public interface IGenericVertexBufferObject : IDisposable {
    string Name { get; }
    VertexAttribPointerType PointerType { get; }
    int Length { get; }
    BufferStorageTarget BufferTarget { get; }
    uint ID { get; }
    int Size { get; }
    uint Divisor { get; }
    bool Normalize { get; }
    bool CastToFloat { get; }
    bool IsIntegralType { get; }
    uint vboID { get; }
  }
}