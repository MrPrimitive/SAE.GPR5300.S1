using MSE.Engine.GameObjects;
using MSE.Engine.Utils;

namespace SAE.GPR5300.S1.Assets.Models {
  public class Sphere {
    public static Sphere Instance => Lazy.Value;
    private static readonly Lazy<Sphere> Lazy = new(() => new Sphere());
    
    public Mesh Mesh { get; set; }
    
    private Sphere() {
      var objWizard = new ObjWizard("spheres.obj");
      Mesh = new Mesh(objWizard.Vertices, objWizard.Indices);
    }
  }
}