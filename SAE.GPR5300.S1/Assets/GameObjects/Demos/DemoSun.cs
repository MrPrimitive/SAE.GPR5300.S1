using System.Drawing;
using System.Numerics;
using MSE.Engine.Extensions;
using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Assets.Models;
using SAE.GPR5300.S1.Assets.Shaders.Materials;
using SAE.GPR5300.S1.Assets.Textures;
using SAE.GPR5300.S1.Core;
using Silk.NET.OpenGL;
using Texture = MSE.Engine.GameObjects.Texture;

namespace SAE.GPR5300.S1.Assets.GameObjects.Demos {
  public class DemoSun : GameObject {
    private Matrix4x4 _matrix;
    private const float Speed = 10;
    private float _rotationMultiplier = 1;
    private float _rotationDegrees;

    public DemoSun() : base(Game.Instance.Gl) {
      Mesh = new Mesh(Game.Instance.Gl, SphereModel.Instance.Vertices, SphereModel.Instance.Indices);
      Material = StandardMaterial.Instance.Material;
      OnLoad();
    }

    public override void OnLoad() {
      Mesh.Textures.Add(new Texture(Gl, TextureFileName.TexSun));
      Transform.Position = new Vector3(0f);
      Transform.Scale = 0.5f;
    }

    public override void UpdateGameObject() {
      _matrix = Transform.ViewMatrix;
    }

    public override void RenderGameObject() {
      Mesh.Bind();
      Material.Use();
      Material.SetBaseValues(_matrix)
        .SetFragColor(Color.White);
      Gl.DrawArrays(PrimitiveType.Triangles, 0, Mesh.IndicesLength);
    }
  }
}