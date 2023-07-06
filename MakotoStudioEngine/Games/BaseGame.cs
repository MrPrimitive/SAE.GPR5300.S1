using MakotoStudioEngine.Scenes;

namespace MakotoStudioEngine.Games {
  public abstract class BaseGame {
    public List<IBaseScene> Scenes => _scenes;

    private List<IBaseScene> _scenes = new();

    public void AddScene(IBaseScene scene) {
      _scenes.Add(scene);
    }
  }
}