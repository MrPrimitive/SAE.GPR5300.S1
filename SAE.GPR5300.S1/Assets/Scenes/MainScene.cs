using MakotoStudioEngine.GameObjects;
using MakotoStudioEngine.Scenes;
using SAE.GPR5300.S1.Assets.GameObjects;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Ui;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;

namespace SAE.GPR5300.S1.Assets.Scenes {
  public class MainScene : BaseScene {
    private Camera _camera;
    private UiScene _uiScene;
    private ImGuiController _imGuiController;

    private SkyBox _skyBox;

    public MainScene(GL gl,
      string sceneName,
      ImGuiController imGuiController,
      Camera camera) : base(gl, sceneName) {
      _camera = camera;
      _imGuiController = imGuiController;
    }

    public new void Init() {
      _uiScene = new UiScene(Gl(), _imGuiController, this);
      var dodecahedron = new Dodecahedron_TestGo(Gl(), _camera);
      AddGameObject(dodecahedron);
      AddUi(_uiScene);
    }
  }
}