using System.Numerics;
using MSE.Engine.Extensions;
using MSE.Engine.GameObjects;
using MSE.Engine.Shaders;
using SAE.GPR5300.S1.Assets.Models;
using SAE.GPR5300.S1.Assets.Shaders.Materials;
using SAE.GPR5300.S1.Assets.Textures;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Ui;
using SAE.GPR5300.S1.Utils;
using Silk.NET.OpenGL;
using Texture = MSE.Engine.GameObjects.Texture;

namespace SAE.GPR5300.S1.Assets.GameObjects.Planets {
  public class Earth : GameObject {
    private ShaderMaterialOptions _shaderMaterialOptions = ShaderMaterialOptions.Defualt;
    private ShaderLightOptions _shaderLightOptions = ShaderLightOptions.Default;
    private Matrix4x4 _matrix;
    private const float Speed = 100;
    private float _rotationDegrees;

    private const float SolarSystemSpeed = 10;
    private float _solarSystemMultiplier = 1;
    private float _rotationSolarSystemDegrees;

    public Earth()
      : base(Game.Instance.Gl) {
      OnLoad();
    }

    public override void OnLoad() {
      Mesh = new Mesh(Game.Instance.Gl, SphereModel.Instance.Vertices, SphereModel.Instance.Indices);
      Material = LightingMaterial.Instance.Material;
      UiSolarSystemSetting.SolarSystemMultiplierEvent += multiplier => _solarSystemMultiplier = multiplier;
      Mesh.Textures.Add(new Texture(Gl, TextureFileName.TexEarth));
      Transform.Position = new Vector3(40, 0, 0);
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
        .SetViewPosition()
        .SetMaterialOptions(_shaderMaterialOptions)
        .SetLightOptions(_shaderLightOptions);
      Gl.DrawArrays(PrimitiveType.Triangles, 0, Mesh.IndicesLength);
    }
  }
}