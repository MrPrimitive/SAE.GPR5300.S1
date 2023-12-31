﻿using MSE.Engine.Utils;
using Silk.NET.OpenGL;

namespace MSE.Engine.Core {
  public class VertexArrayObject<TVertexType, TIndexType> : IDisposable
    where TVertexType : unmanaged
    where TIndexType : unmanaged {
    private uint _handle;
    private GL _gl;

    public VertexArrayObject(GL gl, VertexBufferObject<TVertexType> vbo, VertexBufferObject<TIndexType> ebo) {
      _gl = gl;
      _handle = _gl.GenVertexArray();
      Bind();
      vbo.Bind();
      ebo.Bind();
    }

    public unsafe void VertexAttributePointer(uint index,
      int count,
      VertexAttribPointerType type,
      uint vertexSize,
      int offSet) {
      _gl.VertexAttribPointer(index, count, type, false, vertexSize * (uint)sizeof(TVertexType),
        (void*)(offSet * sizeof(TVertexType)));
      _gl.EnableVertexAttribArray(index);
    }

    public void Bind() {
      _gl.BindVertexArray(_handle);
    }

    public void Dispose() {
      _gl.DeleteVertexArray(_handle);
    }
  }
}