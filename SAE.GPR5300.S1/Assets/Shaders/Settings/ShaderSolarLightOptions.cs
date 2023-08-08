using System.Numerics;

namespace SAE.GPR5300.S1.Assets.Shaders.Settings;

public struct ShaderSolarLightOptions {
  public Vector3 Position { get; set; }
  public Vector3 Ambient { get; set; }
  public Vector3 Diffuse { get; set; }

  public ShaderSolarLightOptions(Vector3 position,
    Vector3 ambient,
    Vector3 diffuse) {
    Position = position;
    Ambient = ambient;
    Diffuse = diffuse;
  }

  static ShaderSolarLightOptions() {
    Default = new ShaderSolarLightOptions(
      new Vector3(0.0f),
      new Vector3(0f),
      new Vector3(1f));
  }

  public static ShaderSolarLightOptions Default { get; }
}