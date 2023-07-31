using System.Numerics;
using MSE.Engine.Extensions;
using MSE.Engine.GameObjects;
using MSE.Engine.Interfaces;
using MSE.Engine.Utils;
using Silk.NET.OpenGL;
using Texture = MSE.Engine.GameObjects.Texture;

namespace MSE.Engine.Core {
  public class SkyCubeBox : ISkyBox {
    // private BufferObject<uint> Ebo;
    // private BufferObject<float> Vbo;
    // private VertexArrayObjectOld<float, uint> VaoCube;

    public Transform Transform { get; set; }
    public Material Material { get; set; }
    public Mesh Mesh { get; set; }

    private Texture _texture;
    private GL _gl;
    private Matrix4x4 _matrix;
    private IModel _model;

    public SkyCubeBox(GL gl,
      Material material,
      IModel model) {
      _model = model;
      Material = material;
      _gl = gl;
      Init();
    }

    private void Init() {
      Mesh = new Mesh(_gl, _model.Vertices, _model.Indices);
      _texture = new Texture(_gl, new List<string> {
        "right.jpg",
        "left.jpg",
        "top.jpg",
        "bottom.jpg",
        "front.jpg",
        "back.jpg"
      });
    }

    public unsafe void Update() {
    }

    public unsafe void Render() {
      _gl.Disable(EnableCap.DepthTest);
      Mesh.BindVAO();
      Material.Use();

      _matrix = Matrix4x4.Identity;
      _matrix *= Matrix4x4.CreateScale(500f);

      Material.SetUniform("uModel", _matrix);
      Material.SetUniform("uView", Camera.Instance.GetViewMatrix());
      Material.SetUniform("uProjection", Camera.Instance.GetProjectionMatrix());

      _gl.DrawArrays(PrimitiveType.Triangles, 0, Mesh.IndicesLength);

      _gl.Enable(EnableCap.DepthTest);
    }
  }
}