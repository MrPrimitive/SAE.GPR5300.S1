using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Core;

namespace SAE.GPR5300.S1.Assets.Shaders.Materials;

public class SolarLightingMaterial {
  public static SolarLightingMaterial Instance => Lazy.Value;
  public readonly Material Material;
  private static readonly Lazy<SolarLightingMaterial> Lazy = new(() => new());

  private SolarLightingMaterial() {
    Material = new Material(Game.Instance.Gl, "vShader_base.vert", "fShader_solarLighting.frag");
  }
}