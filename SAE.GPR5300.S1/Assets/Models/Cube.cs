using MSE.Engine.Interfaces;
using MSE.Engine.Utils;

namespace SAE.GPR5300.S1.Assets.Models {
  public class Cube : IModel {
    public static Cube Instance => Lazy.Value;
    private static readonly Lazy<Cube> Lazy = new(() => new Cube());
    public float[] Vertices { get; }
    public uint[] Indices { get; }

    private Cube() {
      var objWizard = new ObjConverter("cube.obj");
      Vertices = objWizard.Vertices;
      Indices = objWizard.Indices;
    }
  }
}