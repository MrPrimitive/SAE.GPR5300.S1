using System.Numerics;
using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Assets.Materials;

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
      material.SetUniform("light.ambient", shaderLightOptions.Ambient); // ambientColor);
      material.SetUniform("light.diffuse", shaderLightOptions.Diffuse); // diffuseColor); // darkened
      material.SetUniform("light.specular", shaderLightOptions.Specular); //new Vector3(1.0f, 1.0f, 1.0f));
      material.SetUniform("light.position", shaderLightOptions.Position); // LampPosition);
    }
  }
}