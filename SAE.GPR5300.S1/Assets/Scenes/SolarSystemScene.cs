using MakotoStudioEngine.GameObjects;
using MakotoStudioEngine.Scenes;
using MakotoStudioEngine.Utils;
using SAE.GPR5300.S1.Assets.GameObjects;
using SAE.GPR5300.S1.Assets.GameObjects.Planets;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Ui;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;

namespace SAE.GPR5300.S1.Assets.Scenes {
  public class SolarSystemScene : BaseScene {
    private Camera _camera;
    private UITest _uiTest;
    private ImGuiController _imGuiController;

    public SolarSystemScene(GL gl,
      string sceneName,
      ImGuiController imGuiController,
      Camera camera) : base(gl, sceneName) {
      _camera = camera;
      _imGuiController = imGuiController;
    }

    public new void Init() {
      var spheres = new ObjWizard("spheres.obj");
      _uiTest = new UITest(Gl(), _imGuiController);
      var sun = new Sun(Gl(), _camera, spheres);
      AddGameObject(sun);
      
      var mercury = new Mercury(Gl(), _camera, spheres);
      AddGameObject(mercury);
      
      var venus = new Venus(Gl(), _camera, spheres);
      AddGameObject(venus);

      var earth = new Earth(Gl(), _camera, spheres);
      AddGameObject(earth);
      
      var moon = new Moon(Gl(), _camera, earth, spheres);
      AddGameObject(moon);
      
      var mars = new Mars(Gl(), _camera, spheres);
      AddGameObject(mars);
      
      AddUi(_uiTest);
    }
  }
}