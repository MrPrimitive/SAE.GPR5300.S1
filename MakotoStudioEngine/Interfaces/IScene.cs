using MSE.Engine.GameObjects;

namespace MSE.Engine.Interfaces {
  public interface IScene {
    public void LoadScene();
    public string GetSceneName();
    public string GetDescription();
    public void AddGameObject(IGameObject go);
    public void SetSkyBox(ISkyBox skyBox);
    public void AddUi(IUserInterface user);
    public void RenderScene();
    public void UpdateScene();
    public void Unload();
    public void Dispose();
  }
}