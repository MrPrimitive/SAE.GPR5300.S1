namespace MSE.Engine.Interfaces {
  public interface ISceneManager {
    public void AddScene(IScene scene);
    public IScene GetActiveScene();
  }
}