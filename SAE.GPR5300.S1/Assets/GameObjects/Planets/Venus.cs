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
  public class Venus : GameObject {
    private Matrix4x4 _matrix;

    private const float Speed = 219;
    private float _rotationDegrees;

    private const float SolarSystemSpeed = 1.66f;
    private float _solarSystemMultiplier = 1;
    private float _rotationSolarSystemDegrees;

    public Venus() : base(Game.Instance.Gl) {
      Mesh = new Mesh(Game.Instance.Gl, SphereModel.Instance.Vertices, SphereModel.Instance.Indices);
      Material = SolarLightingMaterial.Instance.Material;
      UiSolarSystemSetting.SolarSystemMultiplierEvent += multiplier => _solarSystemMultiplier = multiplier;
      Mesh.Textures.Add(new Texture(Gl, TextureFileName.TexVenus));
      Transform.Position = new Vector3(72.3f, 0, 0);
      Transform.Scale = 0.94f;
    }

    public override void UpdateGameObject() {
      _rotationDegrees = _rotationDegrees.Rotation360(_solarSystemMultiplier * Speed);
      _rotationSolarSystemDegrees = _rotationSolarSystemDegrees.Rotation360(_solarSystemMultiplier * SolarSystemSpeed);
      Transform.Rotation = Transform.RotateY(_rotationDegrees.DegreesToRadians());
      _matrix = Transform.ViewMatrix;
      _matrix *= Matrix4x4.CreateRotationY(_rotationSolarSystemDegrees.DegreesToRadians());
    }

    public override void RenderGameObject() {
      Mesh.Bind();
      Material.Use();
      Material.SetBaseValues(_matrix)
        .SetSolarMaterialOptions(ShaderSolarMaterialOptions.Defualt)
        .SetSolarLightOptions(ShaderSolarLightOptions.Default);
      Gl.DrawArrays(PrimitiveType.Triangles, 0, Mesh.IndicesLength);
    }
  }
}