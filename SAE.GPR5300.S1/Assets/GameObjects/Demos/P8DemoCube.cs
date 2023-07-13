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
  public class P8DemoCube : GameObject {
    private ShaderMaterialOptions _shaderMaterialOptions = ShaderMaterialOptions.Defualt;
    private ShaderLightOptions _shaderLightOptions = ShaderLightOptions.Default;
    private Matrix4x4 _matrix;
    private const float Speed = 10;
    private float _rotationMultiplier = 1;
    private float _rotationDegrees;
    private Vector3 _color = new(0,0,0);

    public P8DemoCube() : base(Game.Instance.Gl) {
      Mesh = new Mesh(Game.Instance.Gl, Cube.Instance.Vertices, Cube.Instance.Indices);
      Material = P8DemoLightingMaterial.Instance.Material;
      UiP8Scene.ColorEvent += color => _color = color;
      OnLoad();
    }

    public override void OnLoad() {
      Mesh.Textures.Add(new Texture(Gl, "crate.jpg"));
      _shaderLightOptions = _shaderLightOptions with {
        Position = new Vector3(4f)
      };
    }

    public override unsafe void UpdateGameObject() {
      _shaderMaterialOptions = _shaderMaterialOptions with {
      };
      _rotationDegrees = _rotationDegrees.Rotation360(_rotationMultiplier * Speed);
      Transform.Rotation = Transform.RotateZ(_rotationDegrees.DegreesToRadians());
      Transform.Rotation *= Transform.RotateY(_rotationDegrees.DegreesToRadians());

      _matrix = Transform.ViewMatrix;
    }

    public override unsafe void RenderGameObject() {
      Mesh.Bind();
      Material.Use();
      Material.SetUniform("uModel", _matrix);
      Material.SetUniform("uView", Camera.Instance.GetViewMatrix());
      Material.SetUniform("uProjection", Camera.Instance.GetProjectionMatrix());
      Material.SetUniform("viewPos", Camera.Instance.Position);
      Material.SetUniform("material.diffuse", _shaderMaterialOptions.Diffuse);
      Material.SetUniform("material.specular", _shaderMaterialOptions.Specular);
      Material.SetUniform("material.shininess", _shaderMaterialOptions.Shininess);
      Material.SetUniform("light.ambient", _shaderLightOptions.Ambient);
      Material.SetUniform("light.diffuse", _shaderLightOptions.Diffuse);
      Material.SetUniform("light.specular", _shaderLightOptions.Specular);
      Material.SetUniform("light.position", _shaderLightOptions.Position);
      
      Material.SetUniform("color", _color);


      Gl.DrawArrays(PrimitiveType.Triangles, 0, Mesh.IndicesLength);
    }
  }
}