using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Core;

namespace SAE.GPR5300.S1.Assets.Shaders.Materials;

public class EarthLightingMaterial {
  public static EarthLightingMaterial Instance => Lazy.Value;
  public readonly Material Material;
  private static readonly Lazy<EarthLightingMaterial> Lazy = new(() => new());

  private EarthLightingMaterial() {
    Material = new Material(Game.Instance.Gl, "vShader_base.vert", "fShader_earthLighting.frag");
  }
}