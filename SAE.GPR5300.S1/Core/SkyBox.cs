using System.Numerics;
using MakotoStudioEngine.Core;
using MakotoStudioEngine.Extensions;
using MakotoStudioEngine.GameObjects;
using MakotoStudioEngine.Utils;
using Silk.NET.OpenGL;
using Texture = MakotoStudioEngine.GameObjects.Texture;

namespace SAE.GPR5300.S1.Core {
  public class SkyBox {
    private BufferObject<uint> Ebo;
    private BufferObject<float> Vbo;
    private VertexArrayObjectOld<float, uint> VaoCube;
    private Texture _texture;
    private Material Material;
    private GL _gl;
    private int _textureId;
    private ObjWizard _objWizard;
    private Camera _camera;

    public SkyBox(GL gl, Camera camera) {
      _gl = gl;
      _camera = camera;
      Init();
    }
    
    private void Init() {
      _objWizard = new ObjWizard("spheres.obj");
      // Material = new Material(_gl, "shaderSkyBox.vert", "shaderSkyBox.frag");
      Material = new Material(_gl, "shader.vert", "shader.frag");
      _texture = new Texture(_gl, "skybox.jpg");
      Ebo = new BufferObject<uint>(_gl, _objWizard.Indices, BufferTargetARB.ElementArrayBuffer);
      Vbo = new BufferObject<float>(_gl, _objWizard.Vertices, BufferTargetARB.ArrayBuffer);
      VaoCube = new VertexArrayObjectOld<float, uint>(_gl, Vbo, Ebo);

      VaoCube.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 8, 0);
      VaoCube.VertexAttributePointer(1, 3, VertexAttribPointerType.Float, 8, 3);
      VaoCube.VertexAttributePointer(2, 2, VertexAttribPointerType.Float, 8, 6);
    }

    public void Update() {
    }

    public void Render() {
      // draw skybox as last
      _gl.DepthMask(false);
      VaoCube.Bind();
      Material.Use();
      var matrix = Matrix4x4.Identity;
      matrix *= Matrix4x4.CreateRotationX(180f.DegreesToRadians());
      matrix *= Matrix4x4.CreateScale(500f);
      
      Material.SetUniform("uModel", matrix);
      Material.SetUniform("uView", _camera.GetViewMatrix());
      Material.SetUniform("uProjection", _camera.GetProjectionMatrix());
      Material.SetUniform("fColor", new Vector3(0.5f, 0.5f, 0.5f));

      _texture.Bind();
      _gl.DrawArrays(PrimitiveType.Triangles, 0, (uint)_objWizard.Indices.Length);

      _gl.DepthMask(true); // set depth function back to default
    }
  }
}