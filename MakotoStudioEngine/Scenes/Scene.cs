using MSE.Engine.Extensions;
using MSE.Engine.Interfaces;

namespace MSE.Engine.Scenes {
  public class Scene : Disposable, IScene {
    public string GetSceneName() => _sceneName;
    public string GetDescription() => _sceneDescription;

    public void AddUi(IUserInterface user) => _uis.Add(user);
    public void AddGameObject(IGameObject go) => _gameObjects.Add(go);
    public void SetSkyBox(ISkyBox skyBox) => _skyBox = skyBox;

    private ISkyBox? _skyBox;
    private readonly string _sceneName;
    private readonly string _sceneDescription;
    private readonly List<IGameObject> _gameObjects = new();
    private readonly List<IUserInterface> _uis = new();

    protected Scene(string sceneName, string sceneDescription) {
      _sceneName = sceneName;
      _sceneDescription = sceneDescription;
    }

    public void LoadScene() {
    }

    public void Unload() {
    }

    public void RenderScene() {
      _skyBox?.Render();
      _gameObjects.RenderGameObjects();
    }

    public void UpdateScene() {
      _skyBox?.Update();
      _gameObjects.UpdateGameObjects();
      _uis.Update();
    }
  }
}