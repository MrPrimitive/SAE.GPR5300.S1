using System.Numerics;

namespace MSE.Engine.Shaders;

public struct ShaderLightOptions {
  public Vector3 Position { get; set; }
  public Vector3 Ambient { get; set; }
  public Vector3 Diffuse { get; set; }
  public Vector3 Specular { get; set; }

  public ShaderLightOptions(Vector3 position,
    Vector3 ambient,
    Vector3 diffuse,
    Vector3 specular) {
    Position = position;
    Ambient = ambient;
    Diffuse = diffuse;
    Specular = specular;
  }

  static ShaderLightOptions() {
    Default = new ShaderLightOptions(
      new Vector3(1.0f),
      new Vector3(1.7f) * new Vector3(0.1f),
      new Vector3(1.7f),
      new Vector3(1.0f));
  }

  public static ShaderLightOptions Default { get; }
}