using MSE.Engine.GameObjects;
using Silk.NET.OpenGL;

namespace MSE.Engine.Interfaces {
  public interface IScene {
    public void Load();
    public string GetSceneName();
    public void AddGameObject(GameObject go);
    public void AddUi(IUiInterface ui);
    public void RenderScene(double deltaTime);
    public void UpdateScene(double deltaTime);
    public List<IUiInterface> Uis();
    public void Unload();
    public void Dispose();
  }
}