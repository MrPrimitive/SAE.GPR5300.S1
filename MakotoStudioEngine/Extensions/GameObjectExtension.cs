using MakotoStudioEngine.GameObjects;

namespace MakotoStudioEngine.Extensions {
  public static class GameObjectExtension {
    public static GameObject AddMaterial(this GameObject go, string vertexPath, string fragmentPath) {
      go.Material = new Material(go.Gl, vertexPath, fragmentPath);
      return go;
    }

    public static void Render(this List<IGameObject> gos, double deltaTime) {
      gos.ForEach(go => go.Render(deltaTime));
    }

    public static void Update(this List<IGameObject> gos, double deltaTime) {
      gos.ForEach(go => go.Update(deltaTime));
    }
  }
}