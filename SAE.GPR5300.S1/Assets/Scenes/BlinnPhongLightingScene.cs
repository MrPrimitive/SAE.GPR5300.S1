using MSE.Engine.Core;
using MSE.Engine.Interfaces;
using MSE.Engine.Scenes;
using SAE.GPR5300.S1.Assets.GameObjects.Demos;
using SAE.GPR5300.S1.Assets.Models;
using SAE.GPR5300.S1.Assets.Shaders.Materials;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Ui;

namespace SAE.GPR5300.S1.Assets.Scenes {
  public class BlinnPhongLightingScene : Scene, IScene {
    private SkyBox _skyBox;
    private bool _instantiate;

    public BlinnPhongLightingScene(string sceneName) : base(sceneName) {
    }

    public new void LoadScene() {
      if (_instantiate)
        return;
      
      SetSkyBox(new SkyBox(Game.Instance.Gl, "skybox_desert", StandardMaterial.Instance.Material, SkyBoxModel.Instance));
      AddGameObject(new BlinnPhongLightingPlane());
      AddUi(new UiSceneManager());
      AddUi(new UiBlinnPhongLightingScene());
      _instantiate = true;
    }

    public new void UnLoad() {
      base.Unload();
      _instantiate = false;
    }

    public new void Dispose() {
      base.Dispose();
    }
  }
}