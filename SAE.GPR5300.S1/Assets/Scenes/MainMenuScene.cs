using MSE.Engine.Core;
using MSE.Engine.Interfaces;
using MSE.Engine.Scenes;
using SAE.GPR5300.S1.Ui;

namespace SAE.GPR5300.S1.Assets.Scenes {
  public class MainMenuScene : Scene, IScene {
    private UiScene _uiScene;
    private SkyBox _skyBox;
    private bool _instantiate;

    public MainMenuScene(string sceneName) : base(sceneName) {
      
    }

    public new void LoadScene() {
      base.LoadScene();
      if (_instantiate)
        return;
      
      AddUi(new UiMainMenu());
      _instantiate = true;
    }

    public new void Unload() {
      base.Unload();
    }

    public new void Dispose() {
      base.Dispose();
    }
  }
}