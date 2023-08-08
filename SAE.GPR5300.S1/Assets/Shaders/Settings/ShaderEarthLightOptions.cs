using System.Numerics;

namespace SAE.GPR5300.S1.Assets.Shaders.Settings;

public struct ShaderEarthLightOptions {
  public Vector3 Position { get; set; }
  public Vector3 Ambient { get; set; }
  public Vector3 Diffuse { get; set; }
  public Vector3 DiffuseNight { get; set; }

  public ShaderEarthLightOptions(Vector3 position,
    Vector3 ambient,
    Vector3 diffuse,
    Vector3 diffuseNight) {
    Position = position;
    Ambient = ambient;
    Diffuse = diffuse;
    DiffuseNight = diffuseNight;
  }

  static ShaderEarthLightOptions() {
    Default = new ShaderEarthLightOptions(
    new Vector3(0.0f),
    new Vector3(0.02f),
    new Vector3(1.62f),
    new Vector3(1.14f));
  }

  public static ShaderEarthLightOptions Default { get; }
}