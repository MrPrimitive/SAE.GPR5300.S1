using MSE.Engine.Interfaces;
using MSE.Engine.Utils;

namespace SAE.GPR5300.S1.Assets.Models {
  public class CubeModel : IModel {
    public static CubeModel Instance => Lazy.Value;
    private static readonly Lazy<CubeModel> Lazy = new(() => new CubeModel());
    public float[] Vertices { get; }
    public uint[] Indices { get; }

    private CubeModel() {
      var objWizard = new ObjConverter("cube.obj");
      Vertices = objWizard.Vertices;
      Indices = objWizard.Indices;
    }
  }
}