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

    public ReflectionScene(string sceneName) : base(sceneName) {
    }

    public new void LoadScene() {
      if (_instantiate)
        return;

      SetSkyBox(new SkyCubeBox(Game.Instance.Gl,
        TextureFileName.TexSkyBoxCubeWaterMountain,
        // TextureFileName.TexSkyBoxCubeAnime,
        SkyCubeBoxMaterial.Instance.Material,
        CubeModel.Instance));
      AddGameObject(new ReflectionDodecahedron());
      AddUi(new UiSceneManager());
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