using System.Numerics;
using MSE.Engine.Core;
using MSE.Engine.Extensions;
using MSE.Engine.GameObjects;
using MSE.Engine.Utils;
using SAE.GPR5300.S1.Assets.Materials;
using SAE.GPR5300.S1.Assets.Models;
using SAE.GPR5300.S1.Core;
using Silk.NET.OpenGL;
using Texture = MSE.Engine.GameObjects.Texture;

namespace SAE.GPR5300.S1.Assets.GameObjects.Planets {
  public class Moon : GameObject {
    private BufferObject<uint> Ebo;
    private BufferObject<float> Vbo;
    private VertexArrayObjectOld<float, uint> VaoCube;
    private Vector3 LampPosition = new Vector3(0, 0, 0);

    public Moon(GameObject parent)
      : base(Game.Instance.Gl) {
      _parent = parent;
      Mesh = new Mesh(Sphere.Instance.Mesh.Vertices, Sphere.Instance.Mesh.Indices);
      Material = LightingMaterial.Instance.Material;
      OnLoad();
    }

    private float roatation = 0;
    private float roatationRound = 0;
    private float speed = 10;
    private float speedRound = 20;
    private GameObject _parent;
    private Matrix4x4 _matrix;

    public override void OnLoad() {
      Mesh.Textures.Add(new Texture(Gl, "moon.png"));
      Mesh.Textures.Add(new Texture(Gl, "moon.png"));

      Ebo = new BufferObject<uint>(Gl, Mesh.Indices, BufferTargetARB.ElementArrayBuffer);
      Vbo = new BufferObject<float>(Gl, Mesh.Vertices, BufferTargetARB.ArrayBuffer);
      VaoCube = new VertexArrayObjectOld<float, uint>(Gl, Vbo, Ebo);

      VaoCube.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 8, 0);
      VaoCube.VertexAttributePointer(1, 3, VertexAttribPointerType.Float, 8, 3);
      VaoCube.VertexAttributePointer(2, 2, VertexAttribPointerType.Float, 8, 6);


      // Transform.Position = new Vector3(2,0,0);
      Transform.Scale = 0.27f;
      // Transform.Scale = 4.27f;
      // Transform.Rotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), 180f.DegreesToRadians());
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

      Transform.Rotation = Transform.RotateY(roatation.DegreesToRadians());
      Transform.Position = new Vector3(2, 0, 0);

      _matrix = Transform.ViewMatrix;
      _matrix *= _parent.Transform.ViewMatrix;

      // around the sun
      _matrix *= Matrix4x4.CreateRotationY(roatationRound.DegreesToRadians());
    }

    public override unsafe void RenderGameObject(double deltaTime) {
      VaoCube.Bind();
      Material.Use();

      var texture1 = Mesh.Textures[0];
      texture1.Bind(TextureUnit.Texture0);
      var texture2 = Mesh.Textures[1];
      texture1.Bind(TextureUnit.Texture2);

      Material.SetUniform("uModel", _matrix);
      Material.SetUniform("uView", Camera.Instance.GetViewMatrix());
      Material.SetUniform("uProjection", Camera.Instance.GetProjectionMatrix());
      //Let the shaders know where the Camera is looking from
      Material.SetUniform("viewPos", Camera.Instance.Position);
      //Configure the materials variables.
      //Diffuse is set to 0 because our diffuseMap is bound to Texture0
      Material.SetUniform("material.diffuse", 0);
      //Specular is set to 1 because our diffuseMap is bound to Texture1
      Material.SetUniform("material.specular", 1);
      Material.SetUniform("material.shininess", 1.0f);

      var diffuseColor = new Vector3(1.7f);
      var ambientColor = diffuseColor * new Vector3(0.1f);

      Material.SetUniform("light.ambient", ambientColor);
      Material.SetUniform("light.diffuse", diffuseColor); // darkened
      Material.SetUniform("light.specular", new Vector3(1.0f, 1.0f, 1.0f));
      Material.SetUniform("light.position", LampPosition);

      Gl.DrawArrays(PrimitiveType.Triangles, 0, Mesh.IndicesLength);
    }
  }
}