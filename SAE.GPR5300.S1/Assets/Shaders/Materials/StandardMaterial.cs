using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Core;

namespace SAE.GPR5300.S1.Assets.Shaders.Materials;

public class StandardMaterial {
  public static StandardMaterial Instance => Lazy.Value;
  public Material Material;
  private static readonly Lazy<StandardMaterial> Lazy = new(() => new());

  private StandardMaterial() {
    Material = new Material(Game.Instance.Gl, "shader.vert", "shader.frag");
  }
}