using MSE.Engine.Core;
using MSE.Engine.Interfaces;
using MSE.Engine.Scenes;
using SAE.GPR5300.S1.Assets.GameObjects.Planets;
using SAE.GPR5300.S1.Assets.Models;
using SAE.GPR5300.S1.Assets.Shaders.Materials;
using SAE.GPR5300.S1.Assets.Textures;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Ui;

namespace SAE.GPR5300.S1.Assets.Scenes {
  public class SolarSystemScene : Scene, IScene {
    private bool _instantiate;

    public SolarSystemScene(string sceneName) : base(sceneName) {
      AddUi(UiSceneManager.Instance);
    }

    public new void LoadScene() {
      if (_instantiate)
        return;

      SetSkyBox(new SkyBox(Game.Instance.Gl, TextureFileName.TexSkyBoxSpace, StandardMaterial.Instance.Material, SkyBoxModel.Instance));
      AddGameObject(new Sun());
      AddGameObject(new Mercury());
      AddGameObject(new Venus());
      var earth = new Earth();
      AddGameObject(earth);
      AddGameObject(new Moon(earth));
      AddGameObject(new Mars());
      AddUi(new UiSolarSystemSetting());
      _instantiate = true;
    }

    public new void Unload() {
      base.Unload();
    }
  }
}