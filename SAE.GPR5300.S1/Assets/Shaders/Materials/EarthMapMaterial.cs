using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Core;

namespace SAE.GPR5300.S1.Assets.Shaders.Materials;

public class EarthMapMaterial {
  public static EarthMapMaterial Instance => Lazy.Value;
  public readonly Material Material;
  private static readonly Lazy<EarthMapMaterial> Lazy = new(() => new());

  private EarthMapMaterial() {
    Material = new Material(Game.Instance.Gl, "vShader_base.vert", "fShader_earthMap.frag");
  }
}