using System.Numerics;
using MSE.Engine.GameObjects;
using MSE.Engine.Shaders;
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

    public static Material SetMaterialOptions(this Material material, ShaderMaterialOptions shaderMaterialOptions) {
      material.SetUniform("material.diffuse", shaderMaterialOptions.Diffuse);
      material.SetUniform("material.specular", shaderMaterialOptions.Specular);
      material.SetUniform("material.shininess", shaderMaterialOptions.Shininess);
      return material;
    }

    public static Material SetLightOptions(this Material material, ShaderLightOptions shaderLightOptions) {
      material.SetUniform("light.ambient", shaderLightOptions.Ambient);
      material.SetUniform("light.diffuse", shaderLightOptions.Diffuse);
      material.SetUniform("light.specular", shaderLightOptions.Specular);
      material.SetUniform("light.position", shaderLightOptions.Position);
      return material;
    }
  }
}