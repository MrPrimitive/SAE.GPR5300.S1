using System.Numerics;
using MSE.Engine.Core;
using MSE.Engine.Extensions;
using MSE.Engine.GameObjects;
using MSE.Engine.Utils;
using SAE.GPR5300.S1.Assets.Models;
using SAE.GPR5300.S1.Assets.Shaders.Materials;
using SAE.GPR5300.S1.Assets.Shaders.Options;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Ui;
using SAE.GPR5300.S1.Utils;
using Silk.NET.OpenGL;
using Texture = MSE.Engine.GameObjects.Texture;

namespace SAE.GPR5300.S1.Assets.GameObjects.Planets {
  public class Mercury : GameObject {
    private ShaderMaterialOptions _shaderMaterialOptions = ShaderMaterialOptions.Defualt;
    private ShaderLightOptions _shaderLightOptions = ShaderLightOptions.Default;
    private Matrix4x4 _matrix;
    private const float Speed = 100;
    private float _rotationDegrees;

    private const float SolarSystemSpeed = 40;
    private float _solarSystemMultiplier = 1;
    private float _rotationSolarSystemDegrees;

    public Mercury()
      : base(Game.Instance.Gl) {
      Mesh = new Mesh(Game.Instance.Gl, Sphere.Instance.Vertices, Sphere.Instance.Indices);
      Material = LightingMaterial.Instance.Material;
      UiSolarSystemSetting.SolarSystemMultiplierEvent += multiplier => _solarSystemMultiplier = multiplier;
      OnLoad();
    }

    public override void OnLoad() {
      Mesh.Textures.Add(new Texture(Gl, "mercury.png"));
      Mesh.Textures.Add(new Texture(Gl, "mercury.png"));
      Transform.Position = new Vector3(20, 0, 0);
      Transform.Scale = 0.38f;
    }

    public override unsafe void UpdateGameObject() {
      _rotationDegrees = _rotationDegrees.Rotation360(_solarSystemMultiplier * Speed);
      _rotationSolarSystemDegrees = _rotationSolarSystemDegrees.Rotation360(_solarSystemMultiplier * SolarSystemSpeed);

      _matrix = Transform.ViewMatrix;
      _matrix *= Matrix4x4.CreateRotationY(_rotationSolarSystemDegrees.DegreesToRadians());
    }

    public override unsafe void RenderGameObject() {
      Mesh.Bind();
      Material.Use();
      LightingShaderUtil.SetShaderValues(Material, _matrix, _shaderMaterialOptions, _shaderLightOptions);
      Gl.DrawArrays(PrimitiveType.Triangles, 0, Mesh.IndicesLength);
    }
  }
}