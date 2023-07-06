using System.Numerics;
using MakotoStudioEngine.GameObjects;
using MakotoStudioEngine.Utils;
using Silk.NET.OpenGL;
using Texture = MakotoStudioEngine.GameObjects.Texture;

namespace SAE.GPR5300.S1.Assets.GameObjects {
  public class LightGo : GameObject {
    private BufferObject<uint> Ebo;
    private BufferObject<float> Vbo;
    // private VertexArrayObject<float, uint> Vao;
    private Vector3 LampPosition = new Vector3(1.2f, 1.0f, 2.0f);
    private ObjWizard _objWizard;

    public LightGo(GL gl, Texture texture)
      : base(gl) {
      Init();
    }

    public override void Init() {
      _objWizard = new ObjWizard("dodecahedron.obj");
      // Ebo = new BufferObject<uint>(Gl, _objWizard.Indices, BufferTargetARB.ElementArrayBuffer);
      // Vbo = new BufferObject<float>(Gl, _objWizard.Vertices, BufferTargetARB.ArrayBuffer);
      // Vao = new VertexArrayObject<float, uint>(Gl, Vbo, Ebo);
      //
      // Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 8, 0);
      // Vao.VertexAttributePointer(1, 3, VertexAttribPointerType.Float, 8, 3);
      // Vao.VertexAttributePointer(2, 2, VertexAttribPointerType.Float, 8, 6);

      Material = new Material(Gl, "shader.vert", "shader.frag");
    }

    public override unsafe void Update(double deltaTime) {
    }

    public override unsafe void Render(double deltaTime) {
      // Ebo = new BufferObject<uint>(Gl, _objWizard.Indices, BufferTargetARB.ElementArrayBuffer);
      // Vbo = new BufferObject<float>(Gl, _objWizard.Vertices, BufferTargetARB.ArrayBuffer);
      // Vao = new VertexArrayObject<float, uint>(Gl, Vbo, Ebo);
      //
      // Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 8, 0);
      // Vao.VertexAttributePointer(1, 3, VertexAttribPointerType.Float, 8, 3);
      // Vao.VertexAttributePointer(2, 2, VertexAttribPointerType.Float, 8, 6);
      //
      // Texture.Bind();
      // Material.Use();
      // var lampMatrix = Matrix4x4.Identity;
      // lampMatrix *= Matrix4x4.CreateScale(0.2f);
      // lampMatrix *= Matrix4x4.CreateTranslation(LampPosition);
      //
      // Material.SetUniform("uModel", lampMatrix);
      // Material.SetUniform("uView", Input.Camera.GetViewMatrix());
      // Material.SetUniform("uProjection", Input.Camera.GetProjectionMatrix());
      //
      // var color = Color.Khaki.ToVector3();
      // Material.SetUniform("fColor", color);
      //
      // Gl.DrawArrays(PrimitiveType.Triangles, 0, (uint)_objWizard.Indices.Length);
    }
  }
}