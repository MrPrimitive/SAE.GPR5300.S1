using System.Numerics;
using MSE.Engine.GameObjects;
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
        _activeScene.LoadScene();
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

      Camera.Instance.Position = new Vector3(0, 0, 50f);
      Camera.Instance.Front = Vector3.UnitZ * -1;
      scene.LoadScene();
      Instance.SetSceneActive(scene.GetSceneName());
      currentScene.Unload();
    }
  }
}