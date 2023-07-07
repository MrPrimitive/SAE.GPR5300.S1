using System.Numerics;
using System.Runtime.InteropServices;
using Silk.NET.OpenGL;

namespace MSE.Engine.Core {
  public class VertexBufferObject<T> : IDisposable where T : unmanaged {
    public GL Gl { get; private set; }
    public uint ID { get; private set; }
    public BufferStorageTarget BufferTarget { get; private set; }
    public int Size { get; private set; }
    public VertexAttribPointerType PointerType { get; private set; }
    public int Count { get; private set; }
    public uint Divisor { get; set; } = 0;
    public bool Normalize { get; set; } = true;
    public bool CastToFloat { get; set; } = true;
    public bool IsIntegralType { get; }

    private static readonly Dictionary<Type, int> TypeComponentSize = new Dictionary<Type, int>() {
      [typeof(sbyte)] = 1,
      [typeof(byte)] = 1,
      [typeof(short)] = 1,
      [typeof(ushort)] = 1,
      [typeof(int)] = 1,
      [typeof(uint)] = 1,
      [typeof(float)] = 1,
      [typeof(double)] = 1,
      [typeof(Vector2)] = 2,
      [typeof(Vector3)] = 3,
      [typeof(Vector4)] = 4,
    };

    private static readonly Dictionary<Type, VertexAttribPointerType> TypeAttribPointerType =
      new Dictionary<Type, VertexAttribPointerType>() {
        [typeof(sbyte)] = VertexAttribPointerType.Byte,
        [typeof(byte)] = VertexAttribPointerType.UnsignedByte,
        [typeof(short)] = VertexAttribPointerType.Short,
        [typeof(ushort)] = VertexAttribPointerType.UnsignedShort,
        [typeof(int)] = VertexAttribPointerType.Int,
        [typeof(uint)] = VertexAttribPointerType.UnsignedInt,
        [typeof(float)] = VertexAttribPointerType.Float,
        [typeof(double)] = VertexAttribPointerType.Double,
        [typeof(Vector2)] = VertexAttribPointerType.Float,
        [typeof(Vector3)] = VertexAttribPointerType.Float,
        [typeof(Vector4)] = VertexAttribPointerType.Float,
      };

    private static readonly HashSet<Type> IntegralTypes = new HashSet<Type>() {
      typeof(sbyte),
      typeof(byte),
      typeof(short),
      typeof(ushort),
      typeof(int),
      typeof(uint),
    };

    public VertexBufferObject(GL gl, T[] data)
      : this(gl, data, BufferStorageTarget.ArrayBuffer, VertexBufferObjectUsage.StaticDraw) {
    }

    public VertexBufferObject(GL gl,
      T[] data,
      BufferStorageTarget target = BufferStorageTarget.ArrayBuffer,
      VertexBufferObjectUsage hint = VertexBufferObjectUsage.StaticDraw) {
      Gl = gl;
      ID = CreateVertexBufferObject<T>(BufferTarget = target, data, hint);

      this.Size = GetTypeComponentSize();
      this.PointerType = GetAttribPointerType();
      this.Count = data.Length;
      this.IsIntegralType = IsTypeIntegral();
    }

    /// <summary>
    /// Get the component size of T.
    /// </summary>
    /// <returns>The component size of T.</returns>
    private int GetTypeComponentSize() {
      return TypeComponentSize[typeof(T)];
    }

    private VertexAttribPointerType GetAttribPointerType() {
      return TypeAttribPointerType[typeof(T)];
    }

    private bool IsTypeIntegral() {
      return IntegralTypes.Contains(typeof(T));
    }

    public uint CreateVertexBufferObject<T>(BufferStorageTarget target,
      [In, Out] T[] data,
      VertexBufferObjectUsage hint)
      where T : unmanaged {
      uint vboHandle = Gl.GenBuffer();
      if (vboHandle == 0) return 0;

      int size = data.Length * Marshal.SizeOf(typeof(T));

      Gl.BindBuffer((GLEnum)target, vboHandle);
      Gl.BufferData<T>((GLEnum)target, (uint)size, data, (BufferUsageARB)hint);
      Gl.BindBuffer((GLEnum)target, 0);
      return vboHandle;
    }

    public void Dispose() {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) {
      if (ID != 0) {
        Gl.DeleteBuffer(ID);
        ID = 0;
      }
    }
  }
}