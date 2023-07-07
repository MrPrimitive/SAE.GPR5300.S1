using MSE.Engine.GameObjects;
using MSE.Engine.Interfaces;

namespace MSE.Engine.Extensions {
  public static class GameObjectExtension {
    public static GameObject AddMaterial(this GameObject go, string vertexPath, string fragmentPath) {
      go.Material = new Material(go.Gl, vertexPath, fragmentPath);
      return go;
    }

    public static void RenderGameObjects(this List<IGameObject> gos, double deltaTime) {
      gos.ForEach(go => go.RenderGameObject(deltaTime));
    }

    public static void UpdateGameObjects(this List<IGameObject> gos, double deltaTime) {
      gos.ForEach(go => go.UpdateGameObject(deltaTime));
    }
  }
}