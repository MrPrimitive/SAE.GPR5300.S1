namespace SAE.GPR5300.S1.Assets.Shaders.Settings;

public struct ShaderEarthMaterialOptions {
  public int Diffuse { get; set; }
  public int DiffuseNightLight { get; set; }

  public ShaderEarthMaterialOptions(int diffuse, int diffuseNightLight) {
    Diffuse = diffuse;
    DiffuseNightLight = diffuseNightLight;
  }

  static ShaderEarthMaterialOptions() {
    Defualt = new ShaderEarthMaterialOptions(0, 1);
  }

  public static ShaderEarthMaterialOptions Defualt { get; }
}