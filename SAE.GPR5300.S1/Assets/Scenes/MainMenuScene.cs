﻿using MSE.Engine.Interfaces;
using MSE.Engine.Scenes;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Ui;

namespace SAE.GPR5300.S1.Assets.Scenes {
  public class MainMenuScene : Scene, IScene {
    private bool _instantiate;

    public MainMenuScene() : base(SceneName.MainMenu, "") {
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
  }
}