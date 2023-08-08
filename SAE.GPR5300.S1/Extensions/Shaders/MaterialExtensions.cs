using System.Numerics;
using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Assets.Shaders.Settings;
using Color = System.Drawing.Color;

namespace SAE.GPR5300.S1.Extensions.Shaders {
  public static class MaterialExtensions {
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

    public static Material SetSolarMaterialOptions(this Material material,
      ShaderSolarMaterialOptions shaderMaterialOptions) {
      material.SetUniform("material.diffuse", shaderMaterialOptions.Diffuse);
      return material;
    }

    public static Material SetSolarLightOptions(this Material material, ShaderSolarLightOptions shaderLightOptions) {
      material.SetUniform("light.ambient", shaderLightOptions.Ambient);
      material.SetUniform("light.diffuse", shaderLightOptions.Diffuse);
      material.SetUniform("light.position", shaderLightOptions.Position);
      return material;
    }

    public static Material SetEarthMaterialOptions(this Material material,
      ShaderEarthMaterialOptions shaderMaterialOptions) {
      material.SetUniform("material.diffuse", shaderMaterialOptions.Diffuse);
      material.SetUniform("material.diffuseNightLight", shaderMaterialOptions.DiffuseNightLight);
      return material;
    }

    public static Material SetEarthLightOptions(this Material material, ShaderEarthLightOptions shaderLightOptions) {
      material.SetUniform("light.ambient", shaderLightOptions.Ambient);
      material.SetUniform("light.diffuse", shaderLightOptions.Diffuse);
      material.SetUniform("light.position", shaderLightOptions.Position);
      material.SetUniform("light.diffuseNight", shaderLightOptions.DiffuseNight);
      return material;
    }
  }
}