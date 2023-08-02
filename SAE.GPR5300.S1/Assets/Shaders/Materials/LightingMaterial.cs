using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Core;

namespace SAE.GPR5300.S1.Assets.Shaders.Materials;

public class LightingMaterial {
  public static LightingMaterial Instance => Lazy.Value;
  public readonly Material Material;
  private static readonly Lazy<LightingMaterial> Lazy = new(() => new());

  private LightingMaterial() {
    Material = new Material(Game.Instance.Gl, "vShader_base.vert", "fShader_lighting.frag");
  }
}