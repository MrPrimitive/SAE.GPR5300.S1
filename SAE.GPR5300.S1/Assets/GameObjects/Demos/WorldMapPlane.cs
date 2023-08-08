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
  public class WorldMapPlane : GameObject {
    private Matrix4x4 _matrix;
    private bool _useNormalMap = true;

    public WorldMapPlane() : base(Game.Instance.Gl) {
      Mesh = new Mesh(Game.Instance.Gl, PlaneModel.Instance.Vertices, PlaneModel.Instance.Indices);
      Material = EarthMapMaterial.Instance.Material;
      UiWorldMap.UseNormalMapEvent += () => {
        _useNormalMap = !_useNormalMap;
      };
      Transform.Position = new Vector3(0f, -20f, 0f);
      Mesh.Textures.Add(new Texture(Gl, TextureFileName.TexEarth));
      Mesh.Textures.Add(new Texture(Gl, TextureFileName.TexEarthNormalMap));
    }

    public override void UpdateGameObject() {
      _matrix = Transform.ViewMatrix;
      _matrix *= Matrix4x4.CreateScale(new Vector3(10, 1, 5));
      _matrix *= Matrix4x4.CreateRotationX(90f.DegreesToRadians());
      _matrix *= Matrix4x4.CreateRotationZ(180f.DegreesToRadians());
    }

    public override void RenderGameObject() {
      Mesh.Bind();
      Material.Use();
      Material.SetBaseValues(_matrix);
      Material.SetUniform("material.diffuse", 0);
      Material.SetUniform("material.normalMap", 1);
      Material.SetUniform("light.ambient", new Vector3(1f));
      Material.SetUniform("light.diffuse", new Vector3(0.5f));
      Material.SetUniform("light.position", new Vector3(0f));
      Material.SetUniform("useNormalMap", _useNormalMap);
      Gl.DrawArrays(PrimitiveType.Triangles, 0, Mesh.IndicesLength);
    }
  }
}