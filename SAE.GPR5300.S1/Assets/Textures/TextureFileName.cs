namespace SAE.GPR5300.S1.Assets.Textures {
  public static class TextureFileName {
    // SkyBox
    public static readonly string TexSkyBoxSpace = "tex_skybox_space.jpg";
    public static readonly string TexSkyBoxDesert = "tex_skybox_desert.jpg";

    // SkyBox Cube
    public static readonly List<string> TexSkyBoxCubeWaterMountain = new() {
      "tex_skybox_water_mountain_right.jpg",
      "tex_skybox_water_mountain_left.jpg",
      "tex_skybox_water_mountain_top.jpg",
      "tex_skybox_water_mountain_bottom.jpg",
      "tex_skybox_water_mountain_front.jpg",
      "tex_skybox_water_mountain_back.jpg"
    };

    // Planet
    public static readonly string TexSun = "tex_sun.jpg";
    public static readonly string TexMercury = "tex_mercury.jpg";
    public static readonly string TexVenus = "tex_venus.jpg";
    public static readonly string TexEarth = "tex_earth.jpg";
    public static readonly string TexEarthLight = "tex_earth_light.jpg";
    public static readonly string TexEarthNormalMap = "tex_earth_normalmap.jpg";
    public static readonly string TexMoon = "tex_moon.jpg";
    public static readonly string TexMars = "tex_mars.jpg";

    // Standard Textures
    public static readonly string TexStandardWood = "tex_s_wood.jpg";
  }
}