using MakotoStudioEngine.Extensions;
using MakotoStudioEngine.GameObjects;
using MakotoStudioEngine.Games;
using MakotoStudioEngine.Interfaces;
using Silk.NET.OpenGL;

namespace MakotoStudioEngine.Scenes {
  public class BaseScene : IBaseScene {
    public GL Gl() => _gl;
    public bool IsActiveScene() => _isActiveScene;
    public string GetSceneName() => _sceneName;
    public void AddUi(IUiInterface ui) => _uis.Add(ui);
    public List<IGameObject> GameObjects => _gameObjects;
    public List<IUiInterface> Uis() => _uis;

    private GL _gl;
    private string _sceneName;
    private List<IGameObject> _gameObjects = new();
    private List<IUiInterface> _uis = new();
    private bool _isActiveScene;

    public BaseScene(GL gl, string sceneName) {
      _gl = gl;
      _sceneName = sceneName;
    }

    public void Init() {
      throw new NotImplementedException();
    }

    public void AddGameObject(GameObject go) {
      _gameObjects.Add(go);
    }

    public void SetActive() {
      _isActiveScene = true;
    }

    public void Render(double deltaTime) {
      GameObjects.Render(deltaTime);
      
      

      // Update UI and render
      // Uis.Update();
      // Uis.Render();
    }

    public void Update(double deltaTime) {
      GameObjects.Update(deltaTime);
    }
  }
}