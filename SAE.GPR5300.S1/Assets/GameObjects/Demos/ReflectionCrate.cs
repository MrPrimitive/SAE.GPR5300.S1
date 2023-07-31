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
  public class ReflectionCrate : GameObject {
    private Matrix4x4 _matrix;
    private const float Speed = 10;
    private float _rotationMultiplier = 1;
    private float _rotationDegrees;
    private Texture _texture;

    public ReflectionCrate() : base(Game.Instance.Gl) {
      Mesh = new Mesh(Game.Instance.Gl, CubeModel.Instance.Vertices, CubeModel.Instance.Indices);
      Material = ReflectionMaterial.Instance.Material;
      Transform.Scale = 9f;
      OnLoad();
    }

    public override void OnLoad() {
      Mesh.Textures.Add(new Texture(Gl, new List<string> {
        "right.jpg",
        "left.jpg",
        "top.jpg",
        "bottom.jpg",
        "front.jpg",
        "back.jpg"
      }));
    }

    public override void UpdateGameObject() {
      _rotationDegrees = _rotationDegrees.Rotation360(_rotationMultiplier * Speed);
      Transform.Rotation = Transform.RotateZ(_rotationDegrees.DegreesToRadians());
      Transform.Rotation *= Transform.RotateY(_rotationDegrees.DegreesToRadians());
      _matrix = Transform.ViewMatrix;
    }

    public override void RenderGameObject() {
      Mesh.BindVAO();
      Material.Use();

      Material.SetUniform("model", _matrix);
      Material.SetUniform("view", Camera.Instance.GetViewMatrix());
      Material.SetUniform("projection", Camera.Instance.GetProjectionMatrix());
      Material.SetUniform("cameraPos", Camera.Instance.Position);

      Gl.DrawArrays(PrimitiveType.Triangles, 0, Mesh.IndicesLength);
    }
  }
}