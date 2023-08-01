using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Core;

namespace SAE.GPR5300.S1.Assets.Shaders.Materials;

public class LightingOptionMaterial {
  public static LightingOptionMaterial Instance => Lazy.Value;
  public Material Material;
  private static readonly Lazy<LightingOptionMaterial> Lazy = new(() => new());

  private LightingOptionMaterial() {
    Material = new Material(Game.Instance.Gl, "shader.vert", "lightingOptions.frag");
  }
}