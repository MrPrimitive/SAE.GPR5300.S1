using System.Numerics;
using MSE.Engine.Core;
using MSE.Engine.Extensions;
using MSE.Engine.GameObjects;
using MSE.Engine.Utils;
using SAE.GPR5300.S1.Assets.Materials;
using SAE.GPR5300.S1.Assets.Models;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Utils;
using Silk.NET.OpenGL;
using Texture = MSE.Engine.GameObjects.Texture;

namespace SAE.GPR5300.S1.Assets.GameObjects.Planets {
  public class Sun : GameObject {
    private Vector3 LampPosition = new Vector3(0, 0, 0);
    private float roatation = 0;
    private float speed = 100;
    private Matrix4x4 _matrix;

    public Sun()
      : base(Game.Instance.Gl) {
      Mesh = new Mesh(Game.Instance.Gl, Sphere.Instance.Mesh.Vertices, Sphere.Instance.Mesh.Indices);
      Mesh.Textures.Add(new Texture(Gl, "sun.png"));
      Material = StandardMaterial.Instance.Material;
      OnLoad();
    }

    public override void OnLoad() {
      Transform.Scale = 5f;
      Transform.Rotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), 180f.DegreesToRadians());
    }

    public override unsafe void UpdateGameObject(double deltaTime) {
      roatation = roatation.Rotation360(speed);
      _matrix = Transform.ViewMatrix;
      _matrix *= Matrix4x4.CreateRotationY(roatation.DegreesToRadians());
    }

    public override unsafe void RenderGameObject(double deltaTime) {
      Mesh.Bind();
      Material.Use();
      StandardShaderUtil.SetModelPosition(Material, _matrix);
      Gl.DrawArrays(PrimitiveType.Triangles, 0, Mesh.IndicesLength);
    }
  }
}