using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Core;

namespace SAE.GPR5300.S1.Assets.Shaders.Materials;

public class P8DemoLightingMaterial {
  public static P8DemoLightingMaterial Instance => Lazy.Value;
  public Material Material;
  private static readonly Lazy<P8DemoLightingMaterial> Lazy = new(() => new());

  private P8DemoLightingMaterial() {
    Material = new Material(Game.Instance.Gl, "P8DemoShader.vert", "P8DemoLighting.frag");
  }
}