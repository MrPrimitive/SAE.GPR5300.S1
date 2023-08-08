using System.Numerics;
using MSE.Engine.Extensions;
using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Assets.Models;
using SAE.GPR5300.S1.Assets.Shaders.Materials;
using SAE.GPR5300.S1.Assets.Shaders.Settings;
using SAE.GPR5300.S1.Assets.Textures;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Extensions.Shaders;
using SAE.GPR5300.S1.Ui.SolarSystemUi;
using SAE.GPR5300.S1.Utils;
using Silk.NET.OpenGL;
using Texture = MSE.Engine.GameObjects.Texture;

namespace SAE.GPR5300.S1.Assets.GameObjects.Planets {
  public class Earth : GameObject {
    private const float Speed = 365;
    private const float SolarSystemSpeed = 1;

    private Matrix4x4 _matrix;
    private float _rotationDegrees;
    private float _solarSystemMultiplier = 1;
    private float _rotationSolarSystemDegrees;
    private ShaderEarthLightOptions _shaderEarthLightOptions = ShaderEarthLightOptions.Default;

    public Earth() : base(Game.Instance.Gl) {
      Mesh = new Mesh(Game.Instance.Gl, SphereModel.Instance.Vertices, SphereModel.Instance.Indices);
      Material = EarthLightingMaterial.Instance.Material;
      UiSolarSystemSetting.SolarSystemMultiplierEvent += multiplier => _solarSystemMultiplier = multiplier;
      UiEarth.ShaderEarthLightOptionsEvent += options => _shaderEarthLightOptions = options;
      Mesh.Textures.Add(new Texture(Gl, TextureFileName.TexEarth));
      Mesh.Textures.Add(new Texture(Gl, TextureFileName.TexEarthLight));
      Transform.Position = new Vector3(100, 0, 0);
    }
    
    public override void UpdateGameObject() {
      _rotationDegrees = _rotationDegrees.Rotation360(_solarSystemMultiplier * Speed);
      _rotationSolarSystemDegrees = _rotationSolarSystemDegrees.Rotation360(_solarSystemMultiplier * SolarSystemSpeed);
      Transform.Rotation = Transform.RotateZ(180f.DegreesToRadians());
      Transform.Rotation *= Transform.RotateY(_rotationDegrees.DegreesToRadians());
      _matrix = Transform.ViewMatrix;
      _matrix *= Matrix4x4.CreateRotationY(_rotationSolarSystemDegrees.DegreesToRadians());
    }

    public override void RenderGameObject() {
      Mesh.Bind();
      Material.Use();
      Material.SetBaseValues(_matrix)
        .SetEarthMaterialOptions(ShaderEarthMaterialOptions.Defualt)
        .SetEarthLightOptions(_shaderEarthLightOptions);
      Gl.DrawArrays(PrimitiveType.Triangles, 0, Mesh.IndicesLength);
    }
  }
}