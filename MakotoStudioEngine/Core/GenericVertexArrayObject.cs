using MSE.Engine.GameObjects;
using MSE.Engine.Interfaces;
using Silk.NET.OpenGL;

namespace MSE.Engine.Core {
  public class GenericVertexArrayObject : IDisposable {
    #region Private Fields

    private static readonly Dictionary<VertexAttribPointerType, DrawElementsType> ValidElementTypes =
      new Dictionary<VertexAttribPointerType, DrawElementsType>() {
        [VertexAttribPointerType.UnsignedByte] = DrawElementsType.UnsignedByte,
        [VertexAttribPointerType.UnsignedShort] = DrawElementsType.UnsignedShort,
        [VertexAttribPointerType.UnsignedInt] = DrawElementsType.UnsignedInt
      };

    #endregion

    #region Generic VBO

    public IGenericVertexBufferObject[] vbos;

    public struct GenericVertexBufferObject<T> : IGenericVertexBufferObject
      where T : unmanaged {
      private readonly VertexBufferObject<T>? vbo;
      private readonly string name;

      public GL Gl {
        get { return vbo.Gl; }
      }

      public uint ID {
        get { return vbo.ID; }
      }

      public uint vboID {
        get { return vbo.ID; }
      }

      public string Name {
        get { return name; }
      }

      public VertexAttribPointerType PointerType {
        get { return vbo.PointerType; }
      }

      public int Length {
        get { return vbo.Count; }
      }

      public BufferStorageTarget BufferTarget {
        get { return vbo.BufferTarget; }
      }

      public int Size {
        get { return vbo.Size; }
      }

      public uint Divisor {
        get { return vbo.Divisor; }
      }

      public bool Normalize {
        get { return vbo.Normalize; }
      }

      public bool CastToFloat {
        get { return vbo.CastToFloat; }
      }

      public bool IsIntegralType {
        get { return vbo.IsIntegralType; }
      }

      public GenericVertexBufferObject(VertexBufferObject<T>? vbo) : this(vbo, string.Empty) {
      }

      public GenericVertexBufferObject(VertexBufferObject<T>? vbo, string name) {
        this.vbo = vbo;
        this.name = name;
      }

      /// <summary>
      /// Deletes the vertex array from the GPU and will also dispose of any child VBOs if (DisposeChildren == true).
      /// </summary>
      public void Dispose() {
        Dispose(true);
      }

      private void Dispose(bool disposing) {
        if (disposing) {
          vbo.Dispose();
        }
      }
    }

    #endregion

    #region Constructor and Destructor

    public GenericVertexArrayObject(GL gl, Material program) {
      Gl = gl;
      Program = program;
      DrawMode = PrimitiveType.Triangles;
    }

    public GenericVertexArrayObject(GL gl, Material program, bool allowIntAsElementType) {
      Gl = gl;
      Program = program;
      DrawMode = PrimitiveType.Triangles;
      this.allowIntAsElementType = allowIntAsElementType;
    }

    public void Init(IGenericVertexBufferObject[] vbos) {
      this.vbos = vbos;
      ID = Gl.GenVertexArray();
      if (ID != 0) {
        Gl.BindVertexArray(ID);
        BindAttributes(Program);
      }

      Gl.BindVertexArray(0);

      Draw = DrawGl;
    }

    ~GenericVertexArrayObject() {
      Dispose(false);
    }

    #endregion

    #region Properties

    private bool disposeChildren = false;
    private DrawElementsType elementType;
    private bool allowIntAsElementType = true;
    private int offset = 0;
    private IntPtr offsetInBytes = IntPtr.Zero;

    public int Offset {
      get { return offset; }
      set {
        offset = value;
        offsetInBytes = (IntPtr)(offset * GetElementSizeInBytes());
      }
    }

    public int VertexCount { get; set; }

    public bool DisposeChildren {
      get { return disposeChildren; }
      set {
        disposeChildren = value;
        DisposeElementArray = value; // TODO:  I think this is bad behaviour
      }
    }

    public bool DisposeElementArray { get; set; }
    public GL Gl { get; private set; }
    public Material Program { get; private set; }
    public PrimitiveType DrawMode { get; set; }
    public uint ID { get; private set; }

    #endregion

    #region Draw Methods (OGL2 and OGL3)

    private int GetElementSizeInBytes() {
      switch (elementType) {
        case DrawElementsType.UnsignedByte:
          return 1;
        case DrawElementsType.UnsignedShort:
          return 2;
        case DrawElementsType.UnsignedInt:
          return 4;
        default:
          throw new Exception($"Unknown enum value. Expected an enum of type {nameof(DrawElementsType)}.");
      }
    }

    public unsafe void BindAttributes(Material program) {
      IGenericVertexBufferObject elementArray = null;

      for (int i = 0; i < vbos.Length; i++) {
        if (vbos[i].BufferTarget == BufferStorageTarget.ElementArrayBuffer) {
          elementArray = vbos[i];

          // To not break compatibility with previous versions of this call,
          // int is allowed as an element type even though the specs don't allow it.
          // All cases where int is used as the default element type have been marked obsolete
          // but until it's completely removed, this will serve to support that use case.
          if (allowIntAsElementType && vbos[i].PointerType == VertexAttribPointerType.Int) {
            elementType = DrawElementsType.UnsignedInt;
          }
          else {
            // Check if the element array can be used as an indice buffer.
            if (!ValidElementTypes.ContainsKey(vbos[i].PointerType)) {
              throw new Exception(
                $"The element buffer must be an unsigned integral type. See {nameof(DrawElementsType)} enum for valid types.");
            }

            elementType = ValidElementTypes[vbos[i].PointerType];
          }

          continue;
        }

        // According to OGL spec then, if there is no location for an attribute, -1 is returned.
        // The same error representation is used here.

        var p = program[vbos[i].Name];

        int loc = p?.Location ?? -1;
        if (loc == -1) throw new Exception(string.Format("Shader did not contain '{0}'.", vbos[i].Name));

        Gl.EnableVertexAttribArray((uint)loc);
        Gl.BindBuffer((GLEnum)vbos[i].BufferTarget, vbos[i].ID);

        if (vbos[i].CastToFloat) {
          Gl.VertexAttribPointer(
            (uint)loc,
            vbos[i].Size,
            (GLEnum)vbos[i].PointerType,
            vbos[i].Normalize,
            0,
            (void*)IntPtr.Zero);
        }
        else if (vbos[i].IsIntegralType) {
          Gl.VertexAttribIPointer((uint)loc, vbos[i].Size, (GLEnum)vbos[i].PointerType, 0, (void*)IntPtr.Zero);
        }
        else if (vbos[i].PointerType == VertexAttribPointerType.Double) {
          Gl.VertexAttribLPointer((uint)loc, vbos[i].Size, (GLEnum)vbos[i].PointerType, 0, (void*)IntPtr.Zero);
        }
        else {
          throw new Exception(
            "VBO shouldn't be cast to float, isn't an integral type and is not a float. No vertex attribute support this combination.");
        }

        // 0 is the divisors default value.
        // No need to set the divisor to its default value.
        if (vbos[i].Divisor != 0) {
          Gl.VertexAttribDivisor((uint)loc, vbos[i].Divisor);
        }
      }

      if (elementArray != null) {
        Gl.BindBuffer((GLEnum)BufferStorageTarget.ElementArrayBuffer, elementArray.ID);
        VertexCount = elementArray.Length;
      }
    }

    public delegate void DrawFunc();

    public delegate void DrawInstancedFunc(int count);

    public DrawFunc Draw;
    public DrawInstancedFunc DrawInstanced;

    private void DrawGl() {
      if (ID == 0 || VertexCount == 0) return;
      Gl.BindVertexArray(ID);
      Gl.DrawElements((GLEnum)DrawMode, (uint)VertexCount, (GLEnum)elementType, offsetInBytes);
      Gl.BindVertexArray(0);
    }

    public void DrawProgram(Material program) {
      BindAttributes(program);
      Gl.DrawElements((GLEnum)DrawMode, (uint)VertexCount, (GLEnum)elementType, offsetInBytes);
    }

    #endregion

    #region IDisposable

    /// <summary>
    /// Deletes the vertex array from the GPU and will also dispose of any child VBOs if (DisposeChildren == true).
    /// </summary>
    public void Dispose() {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) {
      // first try to dispose of the vertex array
      if (ID != 0) {
        Gl.DeleteVertexArray(ID);

        ID = 0;
      }

      // children must be disposed of separately since OpenGL 2.1 will not have a vertex array
      if (DisposeChildren) {
        for (int i = 0; i < vbos.Length; i++) {
          if (vbos[i].BufferTarget == BufferStorageTarget.ElementArrayBuffer && !DisposeElementArray) continue;
          vbos[i].Dispose();
        }
      }
    }

    #endregion
  }
}