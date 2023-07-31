using MSE.Engine.Interfaces;
using MSE.Engine.Utils;

namespace SAE.GPR5300.S1.Assets.Models {
  public class SphereModel : IModel {
    public static SphereModel Instance => Lazy.Value;
    private static readonly Lazy<SphereModel> Lazy = new(() => new SphereModel());
    public float[] Vertices { get; }
    public uint[] Indices { get; }

    private SphereModel() {
      var objConverter = new ObjConverter("spheres.obj");
      Vertices = objConverter.Vertices;
      Indices = objConverter.Indices;
    }
  }
}