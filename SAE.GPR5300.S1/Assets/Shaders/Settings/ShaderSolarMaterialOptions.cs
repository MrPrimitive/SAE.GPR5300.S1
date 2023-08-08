namespace SAE.GPR5300.S1.Assets.Shaders.Settings;

public struct ShaderSolarMaterialOptions {
  public int Diffuse { get; set; }

  public ShaderSolarMaterialOptions(int diffuse) {
    Diffuse = diffuse;
  }

  static ShaderSolarMaterialOptions() {
    Defualt = new ShaderSolarMaterialOptions(0);
  }

  public static ShaderSolarMaterialOptions Defualt { get; }
}