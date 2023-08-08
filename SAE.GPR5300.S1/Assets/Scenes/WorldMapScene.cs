using System.Drawing;
using System.Numerics;
using MSE.Engine.Core;
using MSE.Engine.GameObjects;
using MSE.Engine.Interfaces;
using MSE.Engine.Scenes;
using SAE.GPR5300.S1.Assets.GameObjects.Demos;
using SAE.GPR5300.S1.Assets.Models;
using SAE.GPR5300.S1.Assets.Shaders.Materials;
using SAE.GPR5300.S1.Assets.Textures;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Ui;

namespace SAE.GPR5300.S1.Assets.Scenes {
  public class WorldMapScene : Scene, IScene {
    private bool _instantiate;

    public WorldMapScene() : base(SceneName.WorldMap, "World map with a normal map to show the mountains and valleys") {
    }

    public new void LoadScene() {
      Camera.Instance.Position = new Vector3(0f, 0f, 50f);
      if (_instantiate)
        return;

      var skyBox = new SkyBox(Game.Instance.Gl,
        TextureFileName.TexSkyBoxDesert,
        StandardMaterial.Instance.Material,
        SkyBoxModel.Instance) {
        Color = Color.DarkGray
      };
      SetSkyBox(skyBox);

      AddGameObject(new WorldMapPlane());
      AddUi(UiSceneManager.Instance);
      AddUi(new UiWorldMap());

      _instantiate = true;
    }

    public new void Unload() {
      base.Unload();
    }
  }
}