﻿using MSE.Engine.Core;
using MSE.Engine.Extensions;
using MSE.Engine.GameObjects;
using MSE.Engine.Interfaces;
using Silk.NET.OpenGL;

namespace MSE.Engine.Scenes {
  public class Scene : Disposable, IScene {
    public string GetSceneName() => _sceneName;
    public void AddUi(IUiInterface ui) => _uis.Add(ui);
    public List<IGameObject> GameObjects() => _gameObjects;
    public List<IUiInterface> Uis() => _uis;

    private string _sceneName;
    private ISkyBox _skyBox;
    private List<IGameObject> _gameObjects = new();
    private List<IUiInterface> _uis = new();

    public Scene(string sceneName) {
      _sceneName = sceneName;
    }

    public void LoadScene() {
    }

    public void Unload() {
    }

    public void AddGameObject(GameObject go) {
      _gameObjects.Add(go);
    }

    public void SetSkyBox(ISkyBox skyBox) {
      _skyBox = skyBox;
    }

    public void RenderScene() {
      if (_skyBox != null)
        _skyBox.Render();

      GameObjects().RenderGameObjects();

      Uis().Update();
      Uis().Render();
    }

    public void UpdateScene() {
      GameObjects().UpdateGameObjects();
    }
  }
}