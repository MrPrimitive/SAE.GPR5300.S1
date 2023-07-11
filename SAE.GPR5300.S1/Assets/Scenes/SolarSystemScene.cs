using MSE.Engine.Core;
using MSE.Engine.Interfaces;
using MSE.Engine.Scenes;
using MSE.Engine.Utils;
using SAE.GPR5300.S1.Assets.GameObjects.Planets;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Ui;

namespace SAE.GPR5300.S1.Assets.Scenes {
  public class SolarSystemScene : Scene, IScene {
    private UITest _uiTest;
    private bool _instantiate;

    public SolarSystemScene(string sceneName) : base(sceneName) {
      AddUi(UiSceneManager.Instance);
    }

    public new void LoadScene() {
      if (_instantiate)
        return;

      SetSkyBox(new SkyBox(Game.Instance.Gl, "skybox"));
      _uiTest = new UITest();
      var sun = new Sun();
      AddGameObject(sun);

      var mercury = new Mercury();
      AddGameObject(mercury);

      var venus = new Venus();
      AddGameObject(venus);

      var earth = new Earth();
      AddGameObject(earth);

      var moon = new Moon(earth);
      AddGameObject(moon);

      var mars = new Mars();
      AddGameObject(mars);

      AddUi(_uiTest);

      _instantiate = true;
    }

    public new void Unload() {
      base.Unload();
    }
  }
}