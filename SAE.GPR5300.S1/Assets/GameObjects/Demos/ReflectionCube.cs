using System.Numerics;
using MSE.Engine.Extensions;
using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Assets.Models;
using SAE.GPR5300.S1.Assets.Shaders.Materials;
using SAE.GPR5300.S1.Assets.Textures;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Extensions.Shaders;
using SAE.GPR5300.S1.Ui;
using SAE.GPR5300.S1.Utils;
using Silk.NET.OpenGL;
using Texture = MSE.Engine.GameObjects.Texture;

namespace SAE.GPR5300.S1.Assets.GameObjects.Demos {
  public class ReflectionCube : GameObject {
    private Matrix4x4 _matrix;
    private const float Speed = 5;
    private float _rotationMultiplier = 1;
    private float _rotationDegrees;

    public ReflectionCube() : base(Game.Instance.Gl) {
      Mesh = new Mesh(Game.Instance.Gl, CubeModel.Instance.Vertices, CubeModel.Instance.Indices);
      Material = ReflectionMaterial.Instance.Material;
      Transform.Scale = 10f;
      Mesh.Textures.Add(new Texture(Gl, TextureFileName.TexSkyBoxCubeWaterMountain));
    }

    public override void UpdateGameObject() {
      _rotationDegrees = _rotationDegrees.Rotation360(_rotationMultiplier * Speed);
      Transform.Rotation = Transform.RotateZ(_rotationDegrees.DegreesToRadians());
      Transform.Rotation *= Transform.RotateY(_rotationDegrees.DegreesToRadians());
      _matrix = Transform.ViewMatrix;
    }

    public override void RenderGameObject() {
      Mesh.BindCubeMap();
      Material.Use();
      Material.SetBaseValues(_matrix)
        .SetViewPosition();
      Gl.DrawArrays(PrimitiveType.Triangles, 0, Mesh.IndicesLength);
    }
  }
}