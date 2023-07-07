using Silk.NET.OpenGL;

namespace MSE.Engine.GameObjects {
  public enum ParamType {
    Uniform,
    Attribute
  }

  public class ProgramParam {
    // public GL Gl => _gl;
    public Type Type => _type;
    public int Location => _location;
    public uint Program => _programid;
    public ParamType ParamType => _ptype;
    public string Name => _name;

    private GL _gl;
    private Type _type;
    private int _location;
    private uint _programid;
    private ParamType _ptype;
    private string _name;

    public ProgramParam(GL gl,
      Type type,
      ParamType paramType,
      string name) {
      _gl = gl;
      _type = type;
      _ptype = paramType;
      _name = name;
    }

    public ProgramParam(GL gl,
      Type type,
      ParamType paramType,
      string name,
      uint program,
      int location)
      : this(gl, type, paramType, name) {
      _programid = program;
      _location = location;
    }

    // public void GetLocation(Material Program) {
    //   Program.Use();
    //   if (_programid == 0) {
    //     _programid = Program.ProgramID;
    //     _location = (_ptype == ParamType.Uniform
    //       ? Program.GetUniformLocation(_name)
    //       : Program.GetAttributeLocation(_name));
    //   }
    // }

    // public void SetValue(bool param) {
    //   if (Type != typeof(bool)) throw new Exception(string.Format("SetValue({0}) was given a bool.", Type));
    //   _gl.Uniform1i(_location, (param) ? 1 : 0);
    // }
    //
    // public void SetValue(int param) {
    //   if (Type != typeof(int) && Type != typeof(Texture))
    //     throw new Exception(string.Format("SetValue({0}) was given a int.", Type));
    //   _gl.Uniform1i(_location, param);
    // }
    //
    // public void SetValue(float param) {
    //   if (Type != typeof(float)) throw new Exception(string.Format("SetValue({0}) was given a float.", Type));
    //   _gl.Uniform1f(_location, param);
    // }
    //
    // public void SetValue(Vector2 param) {
    //   if (Type != typeof(Vector2)) throw new Exception(string.Format("SetValue({0}) was given a Vector2.", Type));
    //   _gl.Uniform2f(_location, param.X, param.Y);
    // }
    //
    // public void SetValue(Vector3 param) {
    //   if (Type != typeof(Vector3)) throw new Exception(string.Format("SetValue({0}) was given a Vector3.", Type));
    //   _gl.Uniform3f(_location, param.X, param.Y, param.Z);
    // }
    //
    // public void SetValue(Vector4 param) {
    //   if (Type != typeof(Vector4)) throw new Exception(string.Format("SetValue({0}) was given a Vector4.", Type));
    //   _gl.Uniform4f(_location, param.X, param.Y, param.Z, param.W);
    // }
    //
    // public void SetValue(Matrix3 param) {
    //   if (Type != typeof(Matrix3)) throw new Exception(string.Format("SetValue({0}) was given a Matrix3.", Type));
    //
    //   _gl.UniformMatrix3fv(_location, param);
    // }
    //
    // public void SetValue(Matrix4 param) {
    //   if (Type != typeof(Matrix4)) throw new Exception(string.Format("SetValue({0}) was given a Matrix4.", Type));
    //
    //   _gl.UniformMatrix4fv(_location, param);
    // }
    //
    // public void SetValue(float[] param) {
    //   if (param.Length == 16) {
    //     if (Type != typeof(Matrix4)) throw new Exception(string.Format("SetValue({0}) was given a Matrix4.", Type));
    //     _gl.UniformMatrix4fv(_location, 1, false, param);
    //   }
    //   else if (param.Length == 9) {
    //     if (Type != typeof(Matrix3)) throw new Exception(string.Format("SetValue({0}) was given a Matrix3.", Type));
    //     _gl.UniformMatrix3fv(_location, 1, false, param);
    //   }
    //   else if (param.Length == 4) {
    //     if (Type != typeof(Vector4)) throw new Exception(string.Format("SetValue({0}) was given a Vector4.", Type));
    //     _gl.Uniform4f(_location, param[0], param[1], param[2], param[3]);
    //   }
    //   else if (param.Length == 3) {
    //     if (Type != typeof(Vector3)) throw new Exception(string.Format("SetValue({0}) was given a Vector3.", Type));
    //     _gl.Uniform3f(_location, param[0], param[1], param[2]);
    //   }
    //   else if (param.Length == 2) {
    //     if (Type != typeof(Vector2)) throw new Exception(string.Format("SetValue({0}) was given a Vector2.", Type));
    //     _gl.Uniform2f(_location, param[0], param[1]);
    //   }
    //   else if (param.Length == 1) {
    //     if (Type != typeof(float)) throw new Exception(string.Format("SetValue({0}) was given a float.", Type));
    //     _gl.Uniform1f(_location, param[0]);
    //   }
    //   else {
    //     throw new ArgumentException("param was an unexpected length.", "param");
    //   }
    // }
  }
}