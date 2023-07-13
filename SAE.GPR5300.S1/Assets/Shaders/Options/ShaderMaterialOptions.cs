namespace SAE.GPR5300.S1.Assets.Shaders.Options;

public struct ShaderMaterialOptions {
  public int Diffuse { get; set; }
  public int Specular { get; set; }
  public float Shininess { get; set; }

  public ShaderMaterialOptions(int diffuse, int specular, float shininess) {
    Diffuse = diffuse;
    Specular = specular;
    Shininess = shininess;
  }

  static ShaderMaterialOptions() {
    Defualt = new ShaderMaterialOptions(0, 1, 1.0f);
  }

  public static ShaderMaterialOptions Defualt { get; }
}