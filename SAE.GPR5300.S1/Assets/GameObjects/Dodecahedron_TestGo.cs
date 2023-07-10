using System.Numerics;
using MSE.Engine.Core;
using MSE.Engine.Extensions;
using MSE.Engine.GameObjects;
using MSE.Engine.Utils;
using SAE.GPR5300.S1.Core;
using Silk.NET.OpenGL;
using Texture = MSE.Engine.GameObjects.Texture;

namespace SAE.GPR5300.S1.Assets.GameObjects {
  public class Dodecahedron_TestGo : GameObject {
    private BufferObject<uint> Ebo;
    private BufferObject<float> Vbo;
    private VertexArrayObjectOld<float, uint> VaoCube;
    private Vector3 LampPosition = new Vector3(0, 2.0f, 2);
    private ObjWizard _objWizard;

    private Material Material2;

    public Dodecahedron_TestGo()
      : base(Game.Instance.Gl) {
      _objWizard = new ObjWizard("dodecahedron.obj");
      Mesh = new Mesh(_objWizard.Vertices, _objWizard.Indices);
      OnLoad();
    }

    public override void OnLoad() {
      Mesh.Textures.Add(new Texture(Gl, "dodecahedron.jpg"));
      Mesh.Textures.Add(new Texture(Gl, "dodecahedron.jpg"));

      Ebo = new BufferObject<uint>(Gl, _objWizard.Indices, BufferTargetARB.ElementArrayBuffer);
      Vbo = new BufferObject<float>(Gl, _objWizard.Vertices, BufferTargetARB.ArrayBuffer);
      VaoCube = new VertexArrayObjectOld<float, uint>(Gl, Vbo, Ebo);

      VaoCube.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 8, 0);
      VaoCube.VertexAttributePointer(1, 3, VertexAttribPointerType.Float, 8, 3);
      VaoCube.VertexAttributePointer(2, 2, VertexAttribPointerType.Float, 8, 6);

      Material = new Material(Gl, "shader.vert", "lighting.frag");

      Material2 = new Material(Gl, "shader.vert", "shader.frag");
    }

    public override unsafe void UpdateGameObject(double deltaTime) {
    }

    private float roatation = 0;
    private float speed = 100;

    public override unsafe void RenderGameObject(double deltaTime) {
      VaoCube.Bind();

      Material.Use();

      var texture1 = Mesh.Textures[0];
      texture1.Bind(TextureUnit.Texture0);
      var texture2 = Mesh.Textures[1];
      texture1.Bind(TextureUnit.Texture2);

      roatation = +roatation + (speed * (float)deltaTime);

      if (roatation > 360) {
        roatation = 0;
      }

      //Setup the coordinate systems for our view
      var cubeMatrix = Matrix4x4.Identity;
      cubeMatrix *= Matrix4x4.CreateScale(0.5f);
      cubeMatrix *= Matrix4x4.CreateRotationY(roatation.DegreesToRadians());
      cubeMatrix *= Matrix4x4.CreateRotationX(180f.DegreesToRadians());

      Material.SetUniform("uModel", cubeMatrix);
      Material.SetUniform("uView", Camera.Instance.GetViewMatrix());
      Material.SetUniform("uProjection", Camera.Instance.GetProjectionMatrix());
      //Let the shaders know where the Camera is looking from
      Material.SetUniform("viewPos", Camera.Instance.Position);
      //Configure the materials variables.
      //Diffuse is set to 0 because our diffuseMap is bound to Texture0
      Material.SetUniform("material.diffuse", 0);
      //Specular is set to 1 because our diffuseMap is bound to Texture1
      Material.SetUniform("material.specular", 1);
      Material.SetUniform("material.shininess", 12.0f);

      var diffuseColor = new Vector3(1.7f);
      var ambientColor = diffuseColor * new Vector3(0.1f);

      Material.SetUniform("light.ambient", ambientColor);
      Material.SetUniform("light.diffuse", diffuseColor); // darkened
      Material.SetUniform("light.specular", new Vector3(1.0f, 1.0f, 1.0f));
      Material.SetUniform("light.position", LampPosition);

      Gl.DrawArrays(PrimitiveType.Triangles, 0, Mesh.IndicesLength);

      Material2.Use();

      //The Lamp cube is going to be a scaled down version of the normal cubes verticies moved to a different screen location
      var lampMatrix = Matrix4x4.Identity;
      lampMatrix *= Matrix4x4.CreateScale(0.2f);
      lampMatrix *= Matrix4x4.CreateTranslation(LampPosition);

      Material2.SetUniform("uModel", lampMatrix);
      Material2.SetUniform("uView", Camera.Instance.GetViewMatrix());
      Material2.SetUniform("uProjection", Camera.Instance.GetProjectionMatrix());
      Material2.SetUniform("fColor", new Vector3(1, 1, 1));

      Gl.DrawArrays(PrimitiveType.Triangles, 0, Mesh.IndicesLength);
    }
  }
}