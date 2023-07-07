using System.Numerics;
using MSE.Engine.GameObjects;
using MSE.Engine.Utils;
using Silk.NET.OpenGL;
using Texture = MSE.Engine.GameObjects.Texture;

namespace SAE.GPR5300.S1.Assets.GameObjects {
  public class CubeGo : GameObject {
    private BufferObject<uint> Ebo;
    private BufferObject<float> Vbo;
    private Vector3 LampPosition = new Vector3(1.2f, 1.0f, 2.0f);

    public CubeGo(GL gl, Camera camera)
      : base(gl) {
      
      var objWizard = new ObjWizard("cube.obj");
      Mesh = new Mesh(Gl, objWizard.V3Vertices, objWizard.V3Normals, objWizard.V2Uvs, objWizard.Indices);
      Init();
    }

    public override void Init() {
      Mesh.Textures.Add(new Texture(Gl, "crate.jpg"));
      Mesh.Textures.Add(new Texture(Gl, "crate_normalmap.png"));

      Material = new Material(Gl, "shader.vert", "lighting.frag");

      // var vbos = new List<IGenericVertexBufferObject>();
      // vbos.Add(new GenericVertexArrayObject.GenericVertexBufferObject<Vector3>(
      //   new VertexBufferObject<Vector3>(Gl, Mesh.Vertices), ""));
      // vbos.Add(new GenericVertexArrayObject.GenericVertexBufferObject<Vector3>(
      //   new VertexBufferObject<Vector3>(Gl, Mesh.Normals), ""));
      // vbos.Add(new GenericVertexArrayObject.GenericVertexBufferObject<Vector2>(
      //   new VertexBufferObject<Vector2>(Gl, Mesh.Uvs),""));
      // vbos.Add(new GenericVertexArrayObject.GenericVertexBufferObject<uint>(
      //   new VertexBufferObject<uint>(
      //     Gl,
      //     Mesh.Indices,
      //     BufferStorageTarget.ElementArrayBuffer,
      //     VertexBufferObjectUsage.StaticDraw
      //   )));
      // var vbo = vbos.ToArray();
      // Mesh.VertexArrayObject = new VertexArrayObject(Gl, Material, vbo);

      // Ebo = new BufferObject<uint>(Gl, _objWizard.Indices, BufferTargetARB.ElementArrayBuffer);
      // Vbo = new BufferObject<float>(Gl, _objWizard.Vertices, BufferTargetARB.ArrayBuffer);
      // Vao = new VertexArrayObject<float, uint>(Gl, Vbo, Ebo);
      //
      // Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 8, 0);
      // Vao.VertexAttributePointer(1, 3, VertexAttribPointerType.Float, 8, 3);
      // Vao.VertexAttributePointer(2, 2, VertexAttribPointerType.Float, 8, 6);
    }

    public override unsafe void UpdateGameObject(double deltaTime) {
    }

    public override unsafe void RenderGameObject(double deltaTime) {
      // Ebo = new BufferObject<uint>(Gl, _objWizard.Indices, BufferTargetARB.ElementArrayBuffer);
      // Vbo = new BufferObject<float>(Gl, _objWizard.Vertices, BufferTargetARB.ArrayBuffer);
      // Vao = new VertexArrayObject<float, uint>(Gl, Vbo, Ebo);
      //
      // Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 8, 0);
      // Vao.VertexAttributePointer(1, 3, VertexAttribPointerType.Float, 8, 3);
      // Vao.VertexAttributePointer(2, 2, VertexAttribPointerType.Float, 8, 6);

      //
      // var texture1 = Mesh.Textures[0];
      // texture1.Bind(TextureUnit.Texture0);
      // var texture2 = Mesh.Textures[1];
      // texture1.Bind(TextureUnit.Texture1);
      // Material.Use();
      //
      // //Setup the coordinate systems for our view
      // Material.SetUniform("uModel", Transform.ViewMatrix);
      // Material.SetUniform("uView", Input.Camera.GetViewMatrix());
      // Material.SetUniform("uProjection", Input.Camera.GetProjectionMatrix());
      // //Let the shaders know where the Camera is looking from
      // Material.SetUniform("viewPos", Input.Camera.Position);
      // //Configure the materials variables.
      // //Diffuse is set to 0 because our diffuseMap is bound to Texture0
      // Material.SetUniform("material.diffuse", 1);
      // //Specular is set to 1 because our diffuseMap is bound to Texture1
      // Material.SetUniform("material.specular", 0);
      // Material.SetUniform("material.shininess", 32.0f);
      //
      // var diffuseColor = new Vector3(0.8f);
      // var ambientColor = diffuseColor * new Vector3(0.7f);
      //
      // Material.SetUniform("light.ambient", ambientColor);
      // Material.SetUniform("light.diffuse", diffuseColor); // darkened
      // Material.SetUniform("light.specular", new Vector3(1.0f, 1.0f, 1.0f));
      // Material.SetUniform("light.position", LampPosition);

      // Mesh.VertexArrayObject.Draw();
      //We're drawing with just vertices and no indicies, and it takes 36 verticies to have a six-sided textured cube
      // Gl.DrawArrays(PrimitiveType.Triangles, 0, (uint)_objWizard.Indices.Length);
    }
  }
}