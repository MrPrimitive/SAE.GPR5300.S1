using System.Numerics;
using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Assets.Shaders.Options;

namespace SAE.GPR5300.S1.Utils {
  public static class LightingShaderUtil {
    public static void SetShaderValues(Material material,
      Matrix4x4 matrix,
      ShaderMaterialOptions shaderMaterialOptions,
      ShaderLightOptions shaderLightOptions) {
      material.SetUniform("uModel", matrix);
      material.SetUniform("uView", Camera.Instance.GetViewMatrix());
      material.SetUniform("uProjection", Camera.Instance.GetProjectionMatrix());
      material.SetUniform("viewPos", Camera.Instance.Position);
      material.SetUniform("material.diffuse", shaderMaterialOptions.Diffuse);
      material.SetUniform("material.specular", shaderMaterialOptions.Specular);
      material.SetUniform("material.shininess", shaderMaterialOptions.Shininess);
      material.SetUniform("light.ambient", shaderLightOptions.Ambient);
      material.SetUniform("light.diffuse", shaderLightOptions.Diffuse);
      material.SetUniform("light.specular", shaderLightOptions.Specular);
      material.SetUniform("light.position", shaderLightOptions.Position);
    }
  }
}