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
  }
}