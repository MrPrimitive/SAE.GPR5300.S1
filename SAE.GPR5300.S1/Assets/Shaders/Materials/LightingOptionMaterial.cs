using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Core;

namespace SAE.GPR5300.S1.Assets.Shaders.Materials;

public class LightingOptionMaterial {
  public static LightingOptionMaterial Instance => Lazy.Value;
  public readonly Material Material;
  private static readonly Lazy<LightingOptionMaterial> Lazy = new(() => new());

  private LightingOptionMaterial() {
    Material = new Material(Game.Instance.Gl, "vShader_base.vert", "fShader_lightingOptions.frag");
  }
}