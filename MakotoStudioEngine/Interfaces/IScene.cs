using MSE.Engine.GameObjects;

namespace MSE.Engine.Interfaces {
  public interface IScene {
    public void LoadScene();
    public string GetSceneName();
    public void AddGameObject(GameObject go);
    public void AddUi(IUiInterface ui);
    public void RenderScene();
    public void UpdateScene();
    public List<IUiInterface> Uis();
    public void Unload();
    public void Dispose();
  }
}