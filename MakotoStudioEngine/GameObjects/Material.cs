using Silk.NET.OpenGL;

namespace MakotoStudioEngine.GameObjects {
  public class Material : Shader {
    public Material(GL gl, string vertexPath, string fragmentPath)
      : base(gl, "shaders/" + vertexPath, "shaders/" + fragmentPath) { }

    private Dictionary<string, ProgramParam> shaderParams = new Dictionary<string, ProgramParam>();

    public ProgramParam this[string name] {
      get { return shaderParams.ContainsKey(name) ? shaderParams[name] : null; }
    }
  }
}