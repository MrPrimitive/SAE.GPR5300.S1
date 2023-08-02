using System.Numerics;
using MSE.Engine.Extensions;
using MSE.Engine.GameObjects;
using MSE.Engine.Interfaces;
using MSE.Engine.Utils;
using Silk.NET.OpenGL;
using Texture = MSE.Engine.GameObjects.Texture;

namespace MSE.Engine.Core {
  public class SkyCubeBox : ISkyBox {
    public Transform Transform { get; set; }
    public Material Material { get; set; }
    public Mesh Mesh { get; set; }

    private GL _gl;
    private IModel _model;
    private List<string> _skyBoxCubeTextures;

    public SkyCubeBox(GL gl,
      List<string> skyBoxCubeTextures,
      Material material,
      IModel model) {
      _gl = gl;
      _skyBoxCubeTextures = skyBoxCubeTextures;
      Material = material;
      _model = model;
      Transform = new Transform();
      Init();
    }

    private void Init() {
      Mesh = new Mesh(_gl, _model.Vertices, _model.Indices);
      Mesh.Textures.Add(new Texture(_gl, _skyBoxCubeTextures));
      Transform.Scale = 500f;
    }

    public void Render() {
      _gl.Disable(EnableCap.DepthTest);
      Mesh.BindCubeMap();
      Material.Use();
      Material.SetBaseValues(Transform.ViewMatrix);
      _gl.DrawArrays(PrimitiveType.Triangles, 0, Mesh.IndicesLength);
      _gl.Enable(EnableCap.DepthTest);
    }
  }
}