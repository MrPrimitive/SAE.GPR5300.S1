namespace MSE.Engine.Interfaces {
  public interface IGameObject {
    public void UpdateGameObject(double deltaTime);
    public void RenderGameObject(double deltaTime);

    public void Dispose();
  }
}