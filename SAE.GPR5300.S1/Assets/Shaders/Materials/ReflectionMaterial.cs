using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Core;

namespace SAE.GPR5300.S1.Assets.Shaders.Materials;

public class ReflectionMaterial {
  public static ReflectionMaterial Instance => Lazy.Value;
  public readonly Material Material;
  private static readonly Lazy<ReflectionMaterial> Lazy = new(() => new());

  private ReflectionMaterial() {
    Material = new Material(Game.Instance.Gl, "vShader_reflection.vert", "fShader_reflection.frag");
  }
}