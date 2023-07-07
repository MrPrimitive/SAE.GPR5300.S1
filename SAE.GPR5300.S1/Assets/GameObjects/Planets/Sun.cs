using System.Numerics;
using MSE.Engine.Core;
using MSE.Engine.Extensions;
using MSE.Engine.GameObjects;
using MSE.Engine.Utils;
using SAE.GPR5300.S1.Core;
using Silk.NET.OpenGL;
using Texture = MSE.Engine.GameObjects.Texture;

namespace SAE.GPR5300.S1.Assets.GameObjects.Planets {
  public class Sun : GameObject {
    private BufferObject<uint> Ebo;
    private BufferObject<float> Vbo;
    private VertexArrayObjectOld<float, uint> VaoCube;
    private Vector3 LampPosition = new Vector3(0, 0, 0);
    private ObjWizard _objWizard;
    private float roatation = 0;
    private float speed = 100;
    private Matrix4x4 _matrix;

    public Sun(ObjWizard objWizard)
      : base(Game.Instance.Gl) {
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

    public override unsafe void UpdateGameObject(double deltaTime) {
      roatation = +roatation + (speed * (float)deltaTime);

      if (roatation > 360) {
        roatation = 0;
      }

      //Setup the coordinate systems for our view
      _matrix = Transform.ViewMatrix;
      _matrix *= Matrix4x4.CreateRotationY(roatation.DegreesToRadians());
    }

    public override unsafe void RenderGameObject(double deltaTime) {
      VaoCube.Bind();
      Material.Use();

      var texture1 = Mesh.Textures[0];
      texture1.Bind(TextureUnit.Texture0);

      Material.SetUniform("uModel", _matrix);
      Material.SetUniform("uView", Camera.Instance.GetViewMatrix());
      Material.SetUniform("uProjection", Camera.Instance.GetProjectionMatrix());
      Material.SetUniform("fColor", new Vector3(1, 1, 1));

      Gl.DrawArrays(PrimitiveType.Triangles, 0, (uint)_objWizard.Indices.Length);
    }
  }
}