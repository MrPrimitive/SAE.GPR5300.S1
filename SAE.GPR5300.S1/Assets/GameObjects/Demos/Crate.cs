using System.Numerics;
using MSE.Engine.Extensions;
using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Assets.Models;
using SAE.GPR5300.S1.Assets.Shaders.Materials;
using SAE.GPR5300.S1.Assets.Shaders.Options;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Ui;
using SAE.GPR5300.S1.Utils;
using Silk.NET.OpenGL;
using Texture = MSE.Engine.GameObjects.Texture;

namespace SAE.GPR5300.S1.Assets.GameObjects.Demos {
  public class Crate : GameObject {
    private ShaderMaterialOptions _shaderMaterialOptions = ShaderMaterialOptions.Defualt;
    private ShaderLightOptions _shaderLightOptions = ShaderLightOptions.Default;
    private Matrix4x4 _matrix;
    private const float Speed = 10;
    private float _rotationMultiplier = 1;
    private float _rotationDegrees;
    private Vector3 _color = new(0,0,0);

    public Crate() : base(Game.Instance.Gl) {
      Mesh = new Mesh(Game.Instance.Gl, CubeModel.Instance.Vertices, CubeModel.Instance.Indices);
      Material = LightingMaterial.Instance.Material;
      Transform.Scale = 9f;
      Transform.Position = new Vector3(-10f, -10f, 0f);
      OnLoad();
      
    }

    public override void OnLoad() {
      Mesh.Textures.Add(new Texture(Gl, "crate.jpg"));
      // Mesh.Textures.Add(new Texture(Gl, "silkBoxed.png"));
      // Mesh.Textures.Add(new Texture(Gl, "silkSpecular.png"));
      _shaderLightOptions = _shaderLightOptions with {
        Position = new Vector3(4f)
      };
    }

    public override void UpdateGameObject() {
      _rotationDegrees = _rotationDegrees.Rotation360(_rotationMultiplier * Speed);
      Transform.Rotation = Transform.RotateZ(_rotationDegrees.DegreesToRadians());
      Transform.Rotation *= Transform.RotateY(_rotationDegrees.DegreesToRadians());

      _shaderLightOptions = _shaderLightOptions with {
        Position = new Vector3(0f),
        Ambient = new Vector3(0.2f) * new Vector3(0.1f),
        Diffuse = new Vector3(0.5f),
        Specular = new Vector3(0.1f)
      };

      _matrix = Transform.ViewMatrix;
    }

    public override void RenderGameObject() {
      Mesh.Bind();
      Material.Use();
      LightingShaderUtil.SetShaderValues(Material, _matrix, _shaderMaterialOptions, _shaderLightOptions);
      Gl.DrawArrays(PrimitiveType.Triangles, 0, Mesh.IndicesLength);
    }
  }
}