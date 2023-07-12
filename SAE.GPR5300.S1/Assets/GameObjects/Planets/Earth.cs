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

    private float _rotationDegrees = 0;
    private float _rotationRoundDegrees = 0;
    private float _speed = 100;
    private float _speedRound = 20;
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

    public override unsafe void UpdateGameObject() {
      _rotationDegrees = _rotationDegrees.Rotation360(_speed);
      _rotationRoundDegrees = _rotationRoundDegrees.Rotation360(_speedRound);

      Transform.Rotation = Transform.RotateZ(180f.DegreesToRadians());
      Transform.Rotation *= Transform.RotateY(_rotationDegrees.DegreesToRadians());
      _matrix = Transform.ViewMatrix;
      // around the sun
      _matrix *= Matrix4x4.CreateRotationY(_rotationRoundDegrees.DegreesToRadians());
    }

    public override unsafe void RenderGameObject() {
      Mesh.Bind();
      Material.Use();
      LightingShaderUtil.SetModelPosition(Material, _matrix, _shaderMaterialOptions, _shaderLightOptions);
      Gl.DrawArrays(PrimitiveType.Triangles, 0, (uint)Mesh.Indices.Length);
    }
  }
}