using System.Numerics;
using MSE.Engine.Extensions;
using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Assets.Models;
using SAE.GPR5300.S1.Assets.Shaders.Materials;
using SAE.GPR5300.S1.Assets.Textures;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Ui;
using Silk.NET.OpenGL;
using Texture = MSE.Engine.GameObjects.Texture;

namespace SAE.GPR5300.S1.Assets.GameObjects.Demos {
  public class BlinnPhongLightingPlane : GameObject {
    private Matrix4x4 _matrix;
    private bool _isBlinn;
    private bool _isGamma;
    private float _exponentBlinn = 31f;
    private float _exponentPhong = 8f;

    public BlinnPhongLightingPlane() : base(Game.Instance.Gl) {
      Mesh = new Mesh(Game.Instance.Gl, PlaneModel.Instance.Vertices, PlaneModel.Instance.Indices);
      Material = BlinnPhongLightingMaterial.Instance.Material;
      Transform.Scale = 20f;
      Transform.Position = new Vector3(0f, -20f, 0f);
      UiBlinnPhongLightingScene.IsBlinnEvent += isBlinn => _isBlinn = isBlinn;
      UiBlinnPhongLightingScene.IsGammaEvent += isGama => _isGamma = isGama;
      UiBlinnPhongLightingScene.ExponentBlinnEvent += exponentBlinn => _exponentBlinn = exponentBlinn;
      UiBlinnPhongLightingScene.ExponentPhongEvent += exponentPhong => _exponentPhong = exponentPhong;
      Mesh.Textures.Add(new Texture(Gl, TextureFileName.TexStandardWood));
    }

    public override void UpdateGameObject() {
      _matrix = Transform.ViewMatrix;
    }

    public override void RenderGameObject() {
      Mesh.Bind();
      Material.Use();
      Material.SetBaseValues(_matrix)
        .SetViewPosition();
      Material.SetUniform("lightPos", new Vector3(0f, 1.5f, 0f));
      Material.SetUniform("blinn", _isBlinn);
      Material.SetUniform("isGamma", _isGamma);
      Material.SetUniform("exponentBlinn", _exponentBlinn);
      Material.SetUniform("exponentPhong", _exponentPhong);

      Gl.DrawArrays(PrimitiveType.Triangles, 0, Mesh.IndicesLength);
    }
  }

  public enum BlinnPhongLighting {
    Blinn,
    BlinnPhong
  }
}