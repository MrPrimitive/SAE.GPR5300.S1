using System.Numerics;
using MSE.Engine.Extensions;
using MSE.Engine.GameObjects;
using MSE.Engine.Utils;
using Silk.NET.OpenGL;
using Texture = MSE.Engine.GameObjects.Texture;

namespace MSE.Engine.Core {
  public class SkyBox {
    private BufferObject<uint> Ebo;
    private BufferObject<float> Vbo;
    private VertexArrayObjectOld<float, uint> VaoCube;
    
    public Transform Transform { get; set; }
    public Material Material { get; set; }
    public Mesh Mesh { get; set; }
    
    private Texture _texture;
    private GL _gl;
    private int _textureId;
    private ObjWizard _objWizard;
    private string _textureName;
    private Matrix4x4 _matrix;

    public SkyBox(GL gl, string textureName) {
      _textureName = textureName;
      _gl = gl;
      Init();
    }

    private void Init() {
      _objWizard = new ObjWizard("spheres.obj");
      // Material = new Material(_gl, "shaderSkyBox.vert", "shaderSkyBox.frag");
      Material = new Material(_gl, "shader.vert", "shader.frag");
      _texture = new Texture(_gl, $"{_textureName}.jpg");
      Ebo = new BufferObject<uint>(_gl, _objWizard.Indices, BufferTargetARB.ElementArrayBuffer);
      Vbo = new BufferObject<float>(_gl, _objWizard.Vertices, BufferTargetARB.ArrayBuffer);
      VaoCube = new VertexArrayObjectOld<float, uint>(_gl, Vbo, Ebo);

      VaoCube.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 8, 0);
      VaoCube.VertexAttributePointer(1, 3, VertexAttribPointerType.Float, 8, 3);
      VaoCube.VertexAttributePointer(2, 2, VertexAttribPointerType.Float, 8, 6);
      
      
    }

    public unsafe void Update(double deltaTime) {
    }

    public unsafe void Render(double deltaTime) {
      // draw skybox as last
      _gl.DepthMask(false);
      _gl.Disable(EnableCap.DepthTest);
      
      VaoCube.Bind();
      Material.Use();
      _matrix = Matrix4x4.Identity;
      _matrix *= Matrix4x4.CreateRotationX(180f.DegreesToRadians());
      _matrix *= Matrix4x4.CreateScale(500f);

      Material.SetUniform("uModel", _matrix);
      Material.SetUniform("uView", Camera.Instance.GetViewMatrix());
      Material.SetUniform("uProjection", Camera.Instance.GetProjectionMatrix());
      Material.SetUniform("fColor", new Vector3(0.5f, 0.5f, 0.5f));

      _texture.Bind();
      _gl.DrawArrays(PrimitiveType.Triangles, 0, (uint)_objWizard.Indices.Length);

      _gl.Enable(EnableCap.DepthTest);
      _gl.DepthMask(true); // set depth function back to default
    }
  }
}