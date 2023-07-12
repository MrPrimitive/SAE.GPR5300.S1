using System.Numerics;
using MSE.Engine.GameObjects;

namespace SAE.GPR5300.S1.Utils {
  public static class StandardShaderUtil {
    public static void SetShaderValues(Material material, Matrix4x4 matrix) {
      material.SetUniform("uModel", matrix);
      material.SetUniform("uView", Camera.Instance.GetViewMatrix());
      material.SetUniform("uProjection", Camera.Instance.GetProjectionMatrix());
      material.SetUniform("fColor", new Vector3(1, 1, 1));
    }
  }
}