﻿using MSE.Engine.Extensions;
using MSE.Engine.GameObjects;
using MSE.Engine.Interfaces;
using Silk.NET.OpenGL;
using Color = System.Drawing.Color;
using Texture = MSE.Engine.GameObjects.Texture;

namespace MSE.Engine.Core {
  public class SkyBox : ISkyBox {
    public Transform Transform { get; set; }
    public Material Material { get; set; }
    public Color Color { get; set; } = Color.White;
    public Mesh Mesh { get; set; }

    private GL _gl;
    private IModel _model;
    private readonly float _scale;
    private string _textureName;

    public SkyBox(GL gl,
      string textureName,
      Material material,
      IModel model,
      float scale = 500f) {
      _gl = gl;
      _model = model;
      _scale = scale;
      _textureName = textureName;
      Material = material;
      Transform = new Transform();
      Init();
    }

    private void Init() {
      Mesh = new Mesh(_gl, _model.Vertices, _model.Indices);
      Mesh.Textures.Add(new Texture(_gl, _textureName));
      Transform.Scale = _scale;
      Transform.Rotation = Transform.RotateY(180f.DegreesToRadians());
    }

    public void Update() {
      Transform.Position = Camera.Instance.Position;
    }

    public void Render() {
      _gl.Disable(EnableCap.DepthTest);
      Mesh.Bind();
      Material.Use();
      Material.SetBaseValues(Transform.ViewMatrix)
        .SetFragColor(Color);
      _gl.DrawArrays(PrimitiveType.Triangles, 0, Mesh.IndicesLength);
      _gl.Enable(EnableCap.DepthTest);
    }
  }
}