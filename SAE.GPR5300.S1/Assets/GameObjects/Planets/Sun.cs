using System.Numerics;
using MakotoStudioEngine.Core;
using MakotoStudioEngine.Extensions;
using MakotoStudioEngine.GameObjects;
using MakotoStudioEngine.Utils;
using Silk.NET.OpenGL;
using Texture = MakotoStudioEngine.GameObjects.Texture;

namespace SAE.GPR5300.S1.Assets.GameObjects.Planets {
  public class Sun : GameObject {
    private BufferObject<uint> Ebo;
    private BufferObject<float> Vbo;
    private VertexArrayObjectOld<float, uint> VaoCube;
    private Vector3 LampPosition = new Vector3(0, 0, 0);
    private ObjWizard _objWizard;
    private Camera _camera;

    public Sun(GL gl, Camera camera, ObjWizard objWizard)
      : base(gl) {
      _camera = camera;
      _objWizard = objWizard;
      Mesh = new Mesh(Gl, _objWizard.V3Vertices, _objWizard.V3Normals, _objWizard.V2Uvs, _objWizard.Indices);
      Init();
    }

    public override void Init() {
      Mesh.Textures.Add(new Texture(Gl, "sun.png"));
      Material = new Material(Gl, "shader.vert", "shader.frag");

      Ebo = new BufferObject<uint>(Gl, _objWizard.Indices, BufferTargetARB.ElementArrayBuffer);
      Vbo = new BufferObject<float>(Gl, _objWizard.Vertices, BufferTargetARB.ArrayBuffer);
      VaoCube = new VertexArrayObjectOld<float, uint>(Gl, Vbo, Ebo);

      VaoCube.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 8, 0);
      VaoCube.VertexAttributePointer(1, 3, VertexAttribPointerType.Float, 8, 3);
      VaoCube.VertexAttributePointer(2, 2, VertexAttribPointerType.Float, 8, 6);

      Transform.Scale = 5f;
      Transform.Rotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), 180f.DegreesToRadians());
    }

    public override unsafe void Update(double deltaTime) {
    }

    private float roatation = 0;
    private float speed = 100;

    public override unsafe void Render(double deltaTime) {
      VaoCube.Bind();
      Material.Use();

      var texture1 = Mesh.Textures[0];
      texture1.Bind(TextureUnit.Texture0);

      Material.Use();

      roatation = +roatation + (speed * (float)deltaTime);

      if (roatation > 360) {
        roatation = 0;
      }

      //Setup the coordinate systems for our view
      var matrix = Transform.ViewMatrix;
      matrix *= Matrix4x4.CreateRotationY(roatation.DegreesToRadians());

      Material.SetUniform("uModel", matrix);
      Material.SetUniform("uView", _camera.GetViewMatrix());
      Material.SetUniform("uProjection", _camera.GetProjectionMatrix());
      Material.SetUniform("fColor", new Vector3(1, 1, 1));

      Gl.DrawArrays(PrimitiveType.Triangles, 0, (uint)_objWizard.Indices.Length);
    }
  }
}