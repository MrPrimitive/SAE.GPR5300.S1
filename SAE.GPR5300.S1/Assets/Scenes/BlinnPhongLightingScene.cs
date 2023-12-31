﻿using MSE.Engine.Core;
using MSE.Engine.Interfaces;
using MSE.Engine.Scenes;
using SAE.GPR5300.S1.Assets.GameObjects.Demos;
using SAE.GPR5300.S1.Assets.Models;
using SAE.GPR5300.S1.Assets.Shaders.Materials;
using SAE.GPR5300.S1.Assets.Textures;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Ui;

namespace SAE.GPR5300.S1.Assets.Scenes {
  public class BlinnPhongLightingScene : Scene, IScene {
    private bool _instantiate;

    public BlinnPhongLightingScene() : base(SceneName.BlinnPhongLighting, "Blinn | Blinn Phone Example Light Scene") { }

    public new void LoadScene() {
      if (_instantiate)
        return;

      SetSkyBox(new SkyBox(Game.Instance.Gl,
        TextureFileName.TexSkyBoxDesert,
        StandardMaterial.Instance.Material,
        SkyBoxModel.Instance));
      AddGameObject(new BlinnPhongLightingPlane());
      AddUi(UiSceneManager.Instance);
      AddUi(new UiBlinnPhongLightingScene());
      _instantiate = true;
    }

    public new void Unload() {
      base.Unload();
    }
  }
}