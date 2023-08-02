using MSE.Engine.Interfaces;
using MSE.Engine.Utils;

namespace SAE.GPR5300.S1.Assets.Models {
  public class PlaneModel : IModel {
    public static PlaneModel Instance => Lazy.Value;
    private static readonly Lazy<PlaneModel> Lazy = new(() => new PlaneModel());
    public float[] Vertices { get; }
    public uint[] Indices { get; }

    private PlaneModel() {
      var objConverter = new ObjConverter("m_plane");
      Vertices = objConverter.Vertices;
      Indices = objConverter.Indices;
    }
  }
}