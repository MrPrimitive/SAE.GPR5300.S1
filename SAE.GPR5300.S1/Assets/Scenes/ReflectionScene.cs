using MSE.Engine.Core;
using MSE.Engine.Interfaces;
using MSE.Engine.Scenes;
using SAE.GPR5300.S1.Assets.GameObjects.Demos;
using SAE.GPR5300.S1.Assets.Models;
using SAE.GPR5300.S1.Assets.Shaders.Materials;
using SAE.GPR5300.S1.Assets.Textures;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Ui;

namespace SAE.GPR5300.S1.Assets.Scenes {
  public class ReflectionScene : Scene, IScene {
    private bool _instantiate;

    public ReflectionScene() : base(SceneName.Reflection, "Example of a reflection - NOTE: (Load this scene before \"World Map - Scene\" or restart the application).") {
    }

    public new void LoadScene() {
      if (_instantiate)
        return;

      SetSkyBox(new SkyCubeBox(Game.Instance.Gl,
        TextureFileName.TexSkyBoxCubeWaterMountain,
        SkyCubeBoxMaterial.Instance.Material,
        CubeModel.Instance));
      AddGameObject(new ReflectionCube());
      AddUi(UiSceneManager.Instance);
      _instantiate = true;
    }

    public new void Unload() {
      base.Unload();
    }
  }
}