using System.Numerics;
using MSE.Engine.GameObjects;
using Color = System.Drawing.Color;

namespace MSE.Engine.Extensions {
  public static class MaterialExtensions {
    public static Material SetBaseValues(this Material material, Matrix4x4 matrix) {
      material.SetUniform("uModel", matrix);
      material.SetUniform("uView", Camera.Instance.GetViewMatrix());
      material.SetUniform("uProjection", Camera.Instance.GetProjectionMatrix());
      return material;
    }

    public static Material SetViewPosition(this Material material) {
      material.SetUniform("viewPos", Camera.Instance.Position);
      return material;
    }

    public static Material SetFragColor(this Material material, Color color) {
      material.SetUniform("fColor", new Vector3(color.R, color.G, color.B) / 255);
      return material;
    }
  }
}