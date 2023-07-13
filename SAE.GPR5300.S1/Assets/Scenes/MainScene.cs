using MSE.Engine.Core;
using MSE.Engine.Interfaces;
using MSE.Engine.Scenes;
using SAE.GPR5300.S1.Assets.GameObjects;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Ui;

namespace SAE.GPR5300.S1.Assets.Scenes {
  public class MainScene : Scene, IScene {
    private UiScene _uiScene;
    private SkyBox _skyBox;
    private bool _instantiate;

    public MainScene(string sceneName) : base(sceneName) {
      
    }

    public new void LoadScene() {
      base.LoadScene();
      if (_instantiate)
        return;
      
      AddUi(new UITest());
      
      

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