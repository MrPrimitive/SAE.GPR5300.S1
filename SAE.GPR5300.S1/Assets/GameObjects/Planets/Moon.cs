using System.Numerics;
using MakotoStudioEngine.Core;
using MakotoStudioEngine.Extensions;
using MakotoStudioEngine.GameObjects;
using MakotoStudioEngine.Utils;
using Silk.NET.OpenGL;
using Texture = MakotoStudioEngine.GameObjects.Texture;

namespace SAE.GPR5300.S1.Assets.GameObjects.Planets {
  public class Moon : GameObject {
    private BufferObject<uint> Ebo;
    private BufferObject<float> Vbo;
    private VertexArrayObjectOld<float, uint> VaoCube;
    private Vector3 LampPosition = new Vector3(0, 0, 0);
    private ObjWizard _objWizard;
    private Camera _camera;

    public Moon(GL gl,
      Camera camera,
      GameObject parent,
      ObjWizard objWizard)
      : base(gl) {
      _parent = parent;
      _camera = camera;
      _objWizard = objWizard;

      Mesh = new Mesh(Gl, _objWizard.V3Vertices, _objWizard.V3Normals, _objWizard.V2Uvs, _objWizard.Indices);
      Init();
    }

    private float roatation = 0;
    private float roatationRound = 0;
    private float speed = 10;
    private float speedRound = 20;
    private GameObject _parent;
    private Matrix4x4 _matrix;

    public override void Init() {
      Mesh.Textures.Add(new Texture(Gl, "moon.png"));
      Mesh.Textures.Add(new Texture(Gl, "moon.png"));

      Ebo = new BufferObject<uint>(Gl, _objWizard.Indices, BufferTargetARB.ElementArrayBuffer);
      Vbo = new BufferObject<float>(Gl, _objWizard.Vertices, BufferTargetARB.ArrayBuffer);
      VaoCube = new VertexArrayObjectOld<float, uint>(Gl, Vbo, Ebo);

      VaoCube.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 8, 0);
      VaoCube.VertexAttributePointer(1, 3, VertexAttribPointerType.Float, 8, 3);
      VaoCube.VertexAttributePointer(2, 2, VertexAttribPointerType.Float, 8, 6);

      Material = new Material(Gl, "shader.vert", "lighting.frag");

     // Transform.Position = new Vector3(2,0,0);
      Transform.Scale = 0.27f;
      // Transform.Scale = 4.27f;
      // Transform.Rotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), 180f.DegreesToRadians());
    }

    public override unsafe void Update(double deltaTime) {
      roatation += speed * (float)deltaTime;
      if (roatation > 360) {
        roatation = 0;
      }
      roatationRound += speedRound * (float)deltaTime;
      if (roatationRound > 360) {
        roatationRound = 0;
      }

      Transform.Rotation = Transform.RotateY(roatation.DegreesToRadians());
      Transform.Position = new Vector3(2,0,0);

      _matrix = Transform.ViewMatrix;
      _matrix *= _parent.Transform.ViewMatrix;

      // around the sun
      _matrix *= Matrix4x4.CreateRotationY(roatationRound.DegreesToRadians());
    }

    public override unsafe void Render(double deltaTime) {
      VaoCube.Bind();
      Material.Use();

      var texture1 = Mesh.Textures[0];
      texture1.Bind(TextureUnit.Texture0);
      var texture2 = Mesh.Textures[1];
      texture1.Bind(TextureUnit.Texture2);

      Material.SetUniform("uModel", _matrix);
      Material.SetUniform("uView", _camera.GetViewMatrix());
      Material.SetUniform("uProjection", _camera.GetProjectionMatrix());
      //Let the shaders know where the Camera is looking from
      Material.SetUniform("viewPos", _camera.Position);
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

      Gl.DrawArrays(PrimitiveType.Triangles, 0, (uint)_objWizard.Indices.Length);
    }
  }
}