using System.Numerics;
using MSE.Engine.Extensions;
using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Assets.Materials;
using SAE.GPR5300.S1.Assets.Models;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Utils;
using Silk.NET.OpenGL;
using Texture = MSE.Engine.GameObjects.Texture;

namespace SAE.GPR5300.S1.Assets.GameObjects.Planets {
  public class Earth : GameObject {
    private Vector3 LampPosition = new Vector3(0, 0, 0);

    private float roatation = 0;
    private float roatationRound = 0;
    private float speed = 100;
    private float speedRound = 20;
    private Matrix4x4 _matrix;
    private ShaderMaterialOptions _shaderMaterialOptions = ShaderMaterialOptions.Defualt;
    private ShaderLightOptions _shaderLightOptions = ShaderLightOptions.Default;

    public Earth()
      : base(Game.Instance.Gl) {
      Mesh = new Mesh(Game.Instance.Gl, Sphere.Instance.Mesh.Vertices, Sphere.Instance.Mesh.Indices);
      Material = LightingMaterial.Instance.Material;
      OnLoad();
    }

    public override void OnLoad() {
      Mesh.Textures.Add(new Texture(Gl, "earth.png"));
      Mesh.Textures.Add(new Texture(Gl, "earth.png"));
      Transform.Position = new Vector3(40, 0, 0);
      // Transform.Rotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), 180f.DegreesToRadians());
      // Transform.Rotation = Transform.RotateZ(157f.DegreesToRadians());
      // Transform.Rotation *= Transform.RotateX(90f.DegreesToRadians());
    }

    public override unsafe void UpdateGameObject(double deltaTime) {
      roatation += speed * (float)deltaTime;
      if (roatation > 360) {
        roatation = 0;
      }

      roatationRound += speedRound * (float)deltaTime;
      if (roatationRound > 360) {
        roatationRound = 0;
      }

      Transform.Rotation = Transform.RotateZ(180f.DegreesToRadians());
      Transform.Rotation *= Transform.RotateY(roatation.DegreesToRadians());
      _matrix = Transform.ViewMatrix;
      // around the sun
      _matrix *= Matrix4x4.CreateRotationY(roatationRound.DegreesToRadians());
    }

    public override unsafe void RenderGameObject(double deltaTime) {
      Mesh.Bind();
      Material.Use();
      LightingShaderUtil.SetModelPosition(Material, _matrix, _shaderMaterialOptions, _shaderLightOptions);
      Gl.DrawArrays(PrimitiveType.Triangles, 0, (uint)Mesh.Indices.Length);
    }
  }
}