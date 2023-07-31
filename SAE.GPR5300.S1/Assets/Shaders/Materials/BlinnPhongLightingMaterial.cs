using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Core;

namespace SAE.GPR5300.S1.Assets.Shaders.Materials;

public class BlinnPhongLightingMaterial {
  public static BlinnPhongLightingMaterial Instance => Lazy.Value;
  public Material Material;
  private static readonly Lazy<BlinnPhongLightingMaterial> Lazy = new(() => new());

  private BlinnPhongLightingMaterial() {
    Material = new Material(Game.Instance.Gl, "blinnphong.vert", "blinnphonglighting.frag");
  }
}