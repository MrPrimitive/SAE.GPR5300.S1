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
      AddUi(UiSceneManager.Instance);
    }

    public new void Load() {
      base.Load();
      if (_instantiate)
        return;

      SetSkyBox(new SkyBox(Game.Instance.Gl, "anime_sky"));
      _uiScene = new UiScene(this);
      var dodecahedron = new Dodecahedron_TestGo();
      AddGameObject(dodecahedron);
      AddUi(_uiScene);

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