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
  public class Mars : GameObject {
    private BufferObject<uint> Ebo;
    private BufferObject<float> Vbo;
    private VertexArrayObjectOld<float, uint> VaoCube;
    private Vector3 LampPosition = new Vector3(0, 0, 0);
    private ShaderMaterialOptions _shaderMaterialOptions = ShaderMaterialOptions.Defualt;
    private ShaderLightOptions _shaderLightOptions = ShaderLightOptions.Default;
    private float roatation = 0;
    private float roatationRound = 0;
    private float speed = 100;
    private float speedRound = 25;
    private Matrix4x4 _matrix;

    public Mars()
      : base(Game.Instance.Gl) {
      Mesh = new Mesh(Game.Instance.Gl, Sphere.Instance.Mesh.Vertices, Sphere.Instance.Mesh.Indices);
      Material = LightingMaterial.Instance.Material;
      OnLoad();
    }

    public override void OnLoad() {
      Mesh.Textures.Add(new Texture(Gl, "mars.png"));
      Mesh.Textures.Add(new Texture(Gl, "mars.png"));
      Transform.Position = new Vector3(50, 0, 0);
      Transform.Scale = 0.53f;
    }

    public override unsafe void UpdateGameObject() {
      roatation += speed * Time.DeltaTime;
      if (roatation > 360) {
        roatation = 0;
      }

      roatationRound += speedRound * Time.DeltaTime;
      if (roatationRound > 360) {
        roatationRound = 0;
      }

      _matrix = Transform.ViewMatrix;
      _matrix *= Matrix4x4.CreateRotationY(roatationRound.DegreesToRadians());
    }

    public override unsafe void RenderGameObject() {
      Mesh.Bind();
      Material.Use();
      LightingShaderUtil.SetModelPosition(Material, _matrix, _shaderMaterialOptions, _shaderLightOptions);
      Gl.DrawArrays(PrimitiveType.Triangles, 0, Mesh.IndicesLength);
    }
  }
}