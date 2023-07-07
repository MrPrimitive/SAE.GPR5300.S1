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

    public new void Load() {
      if (_instantiate)
        return;

      SetSkyBox(new SkyBox(Game.Instance.Gl, "skybox"));
      var spheres = new ObjWizard("spheres.obj");
      _uiTest = new UITest();
      var sun = new Sun(spheres);
      AddGameObject(sun);

      var mercury = new Mercury(spheres);
      AddGameObject(mercury);

      var venus = new Venus(spheres);
      AddGameObject(venus);

      var earth = new Earth(spheres);
      AddGameObject(earth);

      var moon = new Moon(earth, spheres);
      AddGameObject(moon);

      var mars = new Mars(spheres);
      AddGameObject(mars);

      AddUi(_uiTest);

      _instantiate = true;
    }

    public new void Unload() {
      base.Unload();
    }
  }
}