using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Core;

namespace SAE.GPR5300.S1.Assets.Shaders.Materials;

public class DemoLightingMaterial {
  public static DemoLightingMaterial Instance => Lazy.Value;
  public readonly Material Material;
  private static readonly Lazy<DemoLightingMaterial> Lazy = new(() => new());

  private DemoLightingMaterial() {
    Material = new Material(Game.Instance.Gl, "vShader_demoShader.vert", "fShader_demoLighting.frag");
  }
}