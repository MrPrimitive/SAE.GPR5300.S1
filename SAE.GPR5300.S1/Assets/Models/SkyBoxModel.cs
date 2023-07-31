using MSE.Engine.Interfaces;
using MSE.Engine.Utils;

namespace SAE.GPR5300.S1.Assets.Models {
  public class SkyBoxModel : IModel {
    public static SkyBoxModel Instance => Lazy.Value;
    private static readonly Lazy<SkyBoxModel> Lazy = new(() => new SkyBoxModel());
    public float[] Vertices { get; }
    public uint[] Indices { get; }

    private SkyBoxModel() {
      var objConverter = new ObjConverter("skybox.obj");
      Vertices = objConverter.Vertices;
      Indices = objConverter.Indices;
    }
  }
}