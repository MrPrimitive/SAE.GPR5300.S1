using MSE.Engine.Interfaces;

namespace MSE.Engine.Core {
  public class SceneManager {
    public static SceneManager Instance => Lazy.Value;
    public List<IScene> Scenes { get; }

    private static readonly Lazy<SceneManager> Lazy = new(() => new SceneManager());
    private IScene _activeScene;

    private SceneManager() {
      Scenes = new List<IScene>();
    }

    public void SetSceneActive(string sceneName) {
      var scene = Scenes.Find(s => s.GetSceneName().Equals(sceneName));
      if (scene != null) {
        _activeScene = scene;
        _activeScene.Load();
        return;
      }

      throw new Exception("Scene not found");
    }

    public void AddScene(IScene scene) {
      Scenes.Add(scene);
    }

    public IScene GetActiveScene() {
      return _activeScene;
    }

    public void LoadScene(IScene scene) {
      Console.WriteLine($"Try Load Scene {scene.GetSceneName()}");

      // TODO: Unload current active scene
      var currentScene = Instance.GetActiveScene();

      scene.Load();
      Instance.SetSceneActive(scene.GetSceneName());
      currentScene.Unload();
    }
  }
}