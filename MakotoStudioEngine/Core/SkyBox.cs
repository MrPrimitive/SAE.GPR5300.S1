using System.Numerics;
using MSE.Engine.Extensions;
using MSE.Engine.GameObjects;
using MSE.Engine.Interfaces;
using MSE.Engine.Utils;
using Silk.NET.OpenGL;
using Texture = MSE.Engine.GameObjects.Texture;

namespace MSE.Engine.Core {
  public class SkyBox {
    // private BufferObject<uint> Ebo;
    // private BufferObject<float> Vbo;
    // private VertexArrayObjectOld<float, uint> VaoCube;

    public Transform Transform { get; set; }
    public Material Material { get; set; }
    public Mesh Mesh { get; set; }

    private Texture _texture;
    private GL _gl;
    private int _textureId;
    private string _textureName;
    private Matrix4x4 _matrix;
    private IModel _model;

    public SkyBox(GL gl,
      string textureName,
      Material material,
      IModel model) {
      _model = model;
      _textureName = textureName;
      Material = material;
      _gl = gl;
      Init();
    }

    private void Init() {
      Mesh = new Mesh(_gl, _model.Vertices, _model.Indices);
      _texture = new Texture(_gl, $"{_textureName}.jpg");
    }

    public unsafe void Update() {
    }

    public unsafe void Render() {
      // draw skybox as last
      // _gl.DepthMask(false);
      _gl.Disable(EnableCap.DepthTest);
      Mesh.Bind();
      Material.Use();

      _matrix = Matrix4x4.Identity;
      _matrix *= Matrix4x4.CreateRotationX(180f.DegreesToRadians());
      _matrix *= Matrix4x4.CreateScale(500f);

      Material.SetUniform("uModel", _matrix);
      Material.SetUniform("uView", Camera.Instance.GetViewMatrix());
      Material.SetUniform("uProjection", Camera.Instance.GetProjectionMatrix());
      Material.SetUniform("fColor", new Vector3(0.5f, 0.5f, 0.5f));

      _texture.Bind();
      _gl.DrawArrays(PrimitiveType.Triangles, 0, Mesh.IndicesLength);

      _gl.Enable(EnableCap.DepthTest);
      // _gl.DepthMask(true); // set depth function back to default
    }
  }
}