using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Core;

namespace SAE.GPR5300.S1.Assets.Shaders.Materials;

public class StandardMaterial {
  public static StandardMaterial Instance => Lazy.Value;
  public readonly Material Material;
  private static readonly Lazy<StandardMaterial> Lazy = new(() => new());

  private StandardMaterial() {
    Material = new Material(Game.Instance.Gl, "vShader_base.vert", "fShader_base.frag");
  }
}