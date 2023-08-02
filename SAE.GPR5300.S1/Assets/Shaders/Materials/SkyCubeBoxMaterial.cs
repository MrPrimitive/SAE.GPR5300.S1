using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Core;

namespace SAE.GPR5300.S1.Assets.Shaders.Materials;

public class SkyCubeBoxMaterial {
  public static SkyCubeBoxMaterial Instance => Lazy.Value;
  public readonly Material Material;
  private static readonly Lazy<SkyCubeBoxMaterial> Lazy = new(() => new());

  private SkyCubeBoxMaterial() {
    Material = new Material(Game.Instance.Gl, "vShader_skyBox.vert", "fShader_skyBox.frag");
  }
}