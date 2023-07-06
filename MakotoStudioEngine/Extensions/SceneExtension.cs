using MakotoStudioEngine.Games;

namespace MakotoStudioEngine.Extensions {
  public static class SceneExtension {
    public static IBaseScene GetActiveScene(this List<IBaseScene> scenes) {
      return scenes.Find(s => s.IsActiveScene());
    }
  }
}