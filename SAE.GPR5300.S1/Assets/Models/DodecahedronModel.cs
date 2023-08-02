using MSE.Engine.Interfaces;
using MSE.Engine.Utils;

namespace SAE.GPR5300.S1.Assets.Models {
  public class DodecahedronModel : IModel {
    public static DodecahedronModel Instance => Lazy.Value;
    private static readonly Lazy<DodecahedronModel> Lazy = new(() => new DodecahedronModel());
    public float[] Vertices { get; }
    public uint[] Indices { get; }

    private DodecahedronModel() {
      var objConverter = new ObjConverter("m_dodecahedron");
      Vertices = objConverter.Vertices;
      Indices = objConverter.Indices;
    }
  }
}